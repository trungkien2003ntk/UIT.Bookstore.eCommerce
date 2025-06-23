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
            var previousPeriodFilter = GetPreviousPeriodFilter(dateFilter.FromDate, dateFilter.ToDate);

            // Base queries
            var usersQuery = dbContext.Users.Where(u => u.Status == UserStatus.Active); var ordersQuery = dbContext.Orders.AsQueryable();

            // Note: Orders don't have a direct BranchId - they are associated with customers
            // For branch-specific analytics, we could filter by warehouse/branch through shipping address if needed
            // Commenting out this filter for now
            // if (request.BranchId.HasValue)
            // {
            //     ordersQuery = ordersQuery.Where(o => o.BranchId == request.BranchId.Value);
            // }

            // Calculate metrics
            var totalCustomersTask = usersQuery.CountAsync(cancellationToken);

            var newCustomersTask = usersQuery
                .Where(u => u.CreationTime >= dateFilter.FromDate && u.CreationTime <= dateFilter.ToDate)
                .CountAsync(cancellationToken);

            var activeCustomersTask = ordersQuery
                .Where(o => o.CreationTime >= dateFilter.FromDate && o.CreationTime <= dateFilter.ToDate)
                .Select(o => o.CustomerId)
                .Distinct()
                .CountAsync(cancellationToken);

            // Calculate customer retention rate
            var retentionRateTask = CalculateCustomerRetentionRate(ordersQuery, dateFilter, previousPeriodFilter, cancellationToken);

            // Calculate average customer value
            var avgCustomerValueTask = CalculateAverageCustomerValue(ordersQuery, dateFilter, cancellationToken);

            // Get customer growth data
            var customerGrowthTask = GetCustomerGrowth(usersQuery, request.GroupBy, dateFilter, cancellationToken);

            await Task.WhenAll(
                totalCustomersTask, newCustomersTask, activeCustomersTask,
                retentionRateTask, avgCustomerValueTask, customerGrowthTask
            );

            var result = new CustomerAnalyticsDto
            {
                TotalCustomers = await totalCustomersTask,
                NewCustomers = await newCustomersTask,
                ActiveCustomers = await activeCustomersTask,
                CustomerRetentionRate = await retentionRateTask,
                AverageCustomerValue = await avgCustomerValueTask,
                CustomerGrowth = await customerGrowthTask
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
    }

    private async Task<decimal> CalculateAverageCustomerValue(
        IQueryable<Order> ordersQuery,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) dateFilter,
        CancellationToken cancellationToken)
    {
        var customerValues = await ordersQuery
            .Where(o => o.CreationTime >= dateFilter.FromDate &&
                       o.CreationTime <= dateFilter.ToDate &&
                       (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received))
            .GroupBy(o => o.CustomerId)
            .Select(g => g.Sum(o => o.Subtotal + o.ShippingFee))
            .ToListAsync(cancellationToken);

        return customerValues.Any() ? customerValues.Average() : 0;
    }

    private async Task<List<CustomerGrowthDto>> GetCustomerGrowth(
        IQueryable<User> usersQuery,
        string groupBy,
        (DateTimeOffset FromDate, DateTimeOffset ToDate) dateFilter,
        CancellationToken cancellationToken)
    {
        var usersInPeriod = usersQuery.Where(u => u.CreationTime >= dateFilter.FromDate && u.CreationTime <= dateFilter.ToDate); var customerGrowth = groupBy.ToLower() switch
        {
            "day" => await usersInPeriod
                .Where(u => u.CreationTime.HasValue)
                .GroupBy(u => u.CreationTime!.Value.Date)
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0 // Will be calculated below
                })
                .OrderBy(c => c.Date)
                .ToListAsync(cancellationToken),

            "week" => await usersInPeriod
                .Where(u => u.CreationTime.HasValue)
                .GroupBy(u => new DateTime(u.CreationTime!.Value.Year, 1, 1)
                    .AddDays((u.CreationTime!.Value.DayOfYear - 1) / 7 * 7))
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToListAsync(cancellationToken),

            "month" => await usersInPeriod
                .Where(u => u.CreationTime.HasValue)
                .GroupBy(u => new DateTime(u.CreationTime!.Value.Year, u.CreationTime!.Value.Month, 1))
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToListAsync(cancellationToken),

            _ => await usersInPeriod
                .Where(u => u.CreationTime.HasValue)
                .GroupBy(u => u.CreationTime!.Value.Date)
                .Select(g => new CustomerGrowthDto
                {
                    Date = g.Key,
                    NewCustomers = g.Count(),
                    TotalCustomers = 0
                })
                .OrderBy(c => c.Date)
                .ToListAsync(cancellationToken)
        };

        // Calculate cumulative total customers
        var runningTotal = await usersQuery.Where(u => u.CreationTime < dateFilter.FromDate).CountAsync(cancellationToken);
        foreach (var growth in customerGrowth)
        {
            runningTotal += growth.NewCustomers;
            growth.TotalCustomers = runningTotal;
        }

        return customerGrowth;
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
