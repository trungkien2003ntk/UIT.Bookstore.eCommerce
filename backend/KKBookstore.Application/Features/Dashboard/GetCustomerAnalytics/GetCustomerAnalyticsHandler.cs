using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetCustomerAnalytics;

public class GetCustomerAnalyticsHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerAnalyticsQuery, Result<CustomerAnalyticsDto>>
{
    public async Task<Result<CustomerAnalyticsDto>> Handle(GetCustomerAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFilter = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            var previousPeriodFilter = GetPreviousPeriodFilter(dateFilter.FromDate, dateFilter.ToDate);            // Base queries
            var usersQuery = dbContext.Users.Where(u => u.Status == UserStatus.Active);
            var ordersQuery = dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                .AsNoTracking();

            // Note: Orders don't have a direct BranchId - they are associated with customers
            // For branch-specific analytics, we could filter by warehouse/branch through shipping address if needed
            // Commenting out this filter for now
            // if (request.BranchId.HasValue)
            // {
            //     ordersQuery = ordersQuery.Where(o => o.BranchId == request.BranchId.Value);
            // }            // Calculate metrics sequentially to avoid DbContext connection issues
            var totalCustomers = await usersQuery.CountAsync(cancellationToken);

            var newCustomers = await usersQuery
                .Where(u => u.CreationTime >= dateFilter.FromDate && u.CreationTime <= dateFilter.ToDate)
                .CountAsync(cancellationToken);

            var activeCustomers = await ordersQuery
                .Where(o => o.CreationTime >= dateFilter.FromDate && o.CreationTime <= dateFilter.ToDate)
                .Select(o => o.CustomerId)
                .Distinct()
                .CountAsync(cancellationToken);

            // Calculate customer retention rate
            var retentionRate = await CalculateCustomerRetentionRate(ordersQuery, dateFilter, previousPeriodFilter, cancellationToken);

            // Calculate average customer value
            var avgCustomerValue = await CalculateAverageCustomerValue(ordersQuery, dateFilter, cancellationToken);

            // Get customer growth data
            var customerGrowth = await GetCustomerGrowth(usersQuery, request.GroupBy, dateFilter, cancellationToken);

            var result = new CustomerAnalyticsDto
            {
                TotalCustomers = totalCustomers,
                NewCustomers = newCustomers,
                ActiveCustomers = activeCustomers,
                CustomerRetentionRate = retentionRate,
                AverageCustomerValue = avgCustomerValue,
                CustomerGrowth = customerGrowth
            };

            return Result<CustomerAnalyticsDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.CustomerAnalyticsError", $"Error retrieving customer analytics: {ex.Message}");
            return Result.Failure<CustomerAnalyticsDto>(error);
        }
    }

    private async Task<double> CalculateCustomerRetentionRate(
        IQueryable<Order> ordersQuery,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) currentPeriod,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) previousPeriod,
        CancellationToken cancellationToken)
    {
        // Get customers who ordered in previous period
        var previousPeriodCustomers = await ordersQuery
            .Where(o => o.CreationTime >= previousPeriod.FromDate && o.CreationTime <= previousPeriod.ToDate)
            .Select(o => o.CustomerId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (!previousPeriodCustomers.Any())
            return 0;

        // Get customers who ordered in current period and were also in previous period
        var retainedCustomers = await ordersQuery
            .Where(o => o.CreationTime >= currentPeriod.FromDate &&
                       o.CreationTime <= currentPeriod.ToDate &&
                       previousPeriodCustomers.Contains(o.CustomerId))
            .Select(o => o.CustomerId)
            .Distinct()
            .CountAsync(cancellationToken);

        return (double)retainedCustomers / previousPeriodCustomers.Count * 100;
    }    private async Task<decimal> CalculateAverageCustomerValue(
        IQueryable<Order> ordersQuery,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) dateFilter,
        CancellationToken cancellationToken)
    {
        // Get orders with their order lines to calculate customer values
        var customerOrders = await ordersQuery
            .Where(o => o.CreationTime >= dateFilter.FromDate &&
                       o.CreationTime <= dateFilter.ToDate &&
                       (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received))
            .Select(o => new
            {
                o.CustomerId,
                o.ShippingFee,
                OrderLines = o.OrderLines.Select(ol => new
                {
                    ol.Quantity,
                    ol.RecommendedRetailPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        // Calculate customer values in memory
        var customerValues = customerOrders
            .GroupBy(o => o.CustomerId)
            .Select(g => g.Sum(o => o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice)))
            .ToList();

        return customerValues.Any() ? customerValues.Average() : 0;
    }private async Task<List<CustomerGrowthDto>> GetCustomerGrowth(
        IQueryable<User> usersQuery,
        string groupBy,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) dateFilter,
        CancellationToken cancellationToken)
    {
        // Get all users in the period with their creation dates
        var usersInPeriod = await usersQuery
            .Where(u => u.CreationTime >= dateFilter.FromDate && 
                       u.CreationTime <= dateFilter.ToDate && 
                       u.CreationTime.HasValue)
            .Select(u => u.CreationTime!.Value)
            .ToListAsync(cancellationToken);

        // Group and calculate in memory to avoid EF translation issues
        var customerGrowth = groupBy.ToLower() switch
        {
            "day" => usersInPeriod
                .GroupBy(date => date.Date)
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0 // Will be calculated below
                })
                .OrderBy(c => c.Date)
                .ToList(),

            "week" => usersInPeriod
                .GroupBy(date => GetWeekStartDate(date))
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToList(),

            "month" => usersInPeriod
                .GroupBy(date => new DateTime(date.Year, date.Month, 1))
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToList(),

            _ => usersInPeriod
                .GroupBy(date => date.Date)
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToList()
        };

        // Calculate cumulative total customers
        var runningTotal = await usersQuery
            .Where(u => u.CreationTime < dateFilter.FromDate)
            .CountAsync(cancellationToken);
            
        foreach (var growth in customerGrowth)
        {
            runningTotal += growth.NewCustomers;
            growth.TotalCustomers = runningTotal;
        }

        return customerGrowth;
    }

    private static DateTime GetWeekStartDate(DateTimeOffset date)
    {
        var year = date.Year;
        var dayOfYear = date.DayOfYear;
        var weekNumber = (dayOfYear - 1) / 7;
        return new DateTime(year, 1, 1).AddDays(weekNumber * 7);
    }

    private static (DateTimeOffset FromDate, DateTimeOffset ToDate) GetDateFilter(
        string period,
        DateTimeOffset? fromDate,
        DateTimeOffset? toDate)
    {
        var now = DateTimeOffset.Now;

        if (fromDate.HasValue && toDate.HasValue)
        {
            return (fromDate.Value, toDate.Value);
        }

        return period.ToLower() switch
        {
            "week" => (now.AddDays(-7), now),
            "month" => (now.AddMonths(-1), now),
            "year" => (now.AddYears(-1), now),
            _ => (now.AddMonths(-1), now)
        };
    }

    private static (DateTimeOffset FromDate, DateTimeOffset ToDate) GetPreviousPeriodFilter(
        DateTimeOffset currentFromDate,
        DateTimeOffset currentToDate)
    {
        var periodLength = currentToDate - currentFromDate;
        return (currentFromDate - periodLength, currentFromDate);
    }
}
