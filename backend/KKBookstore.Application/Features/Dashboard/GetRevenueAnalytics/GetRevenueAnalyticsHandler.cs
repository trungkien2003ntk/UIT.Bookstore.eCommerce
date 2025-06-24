using System.Linq.Expressions;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetRevenueAnalytics;

public class GetRevenueAnalyticsHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetRevenueAnalyticsQuery, Result<RevenueAnalyticsDto>>
{    public async Task<Result<RevenueAnalyticsDto>> Handle(GetRevenueAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (FromDate, ToDate) = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            var previousPeriodFilter = GetPreviousPeriodFilter(FromDate, ToDate);

            // Build base query with includes
            var baseQuery = dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .AsNoTracking();

            // Apply status filter
            var statusFilter = new[] { OrderStatus.Delivered, OrderStatus.Received };

            // Build current period query
            var currentPeriodQuery = baseQuery
                .Where(o => o.CreationTime >= FromDate &&
                           o.CreationTime <= ToDate &&
                           statusFilter.Contains(o.Status));

            // Build previous period query
            var previousPeriodQuery = baseQuery
                .Where(o => o.CreationTime >= previousPeriodFilter.FromDate &&
                           o.CreationTime <= previousPeriodFilter.ToDate &&
                           statusFilter.Contains(o.Status));

            // Apply product type filter if specified
            if (request.ProductTypeIds.Any())
            {
                currentPeriodQuery = currentPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
                previousPeriodQuery = previousPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
            }            // Execute queries sequentially to avoid DbContext connection issues
            var totalRevenue = await CalculateTotalRevenue(currentPeriodQuery, cancellationToken);
            var previousPeriodRevenue = await CalculateTotalRevenue(previousPeriodQuery, cancellationToken);
            var revenueByPeriod = await GetRevenueByPeriod(currentPeriodQuery, request.GroupBy, cancellationToken);
            
            var growthPercentage = CalculateGrowthPercentage(totalRevenue, previousPeriodRevenue);

            var result = new RevenueAnalyticsDto
            {
                TotalRevenue = totalRevenue,
                PreviousPeriodRevenue = previousPeriodRevenue,
                GrowthPercentage = growthPercentage,
                RevenueByPeriod = revenueByPeriod
            };

            return Result<RevenueAnalyticsDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.GetRevenue", ex.Message);
            return Result.Failure<RevenueAnalyticsDto>(error);
        }
    }    private async Task<decimal> CalculateTotalRevenue(IQueryable<Order> ordersQuery, CancellationToken cancellationToken)
    {
        // Get all orders with their line items to calculate revenue in memory
        // This avoids EF Core translation issues with complex aggregations
        var orders = await ordersQuery
            .Select(o => new
            {
                o.ShippingFee,
                OrderLines = o.OrderLines.Select(ol => new
                {
                    ol.Quantity,
                    ol.RecommendedRetailPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        // Calculate total revenue in memory
        var totalRevenue = orders.Sum(o => 
            o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice));

        return totalRevenue;
    }    private async Task<List<RevenueByPeriodDto>> GetRevenueByPeriod(
        IQueryable<Order> ordersQuery,
        string groupBy,
        CancellationToken cancellationToken)
    {
        // Get orders with minimal data needed for grouping and revenue calculation
        var orders = await ordersQuery
            .Where(o => o.CreationTime.HasValue)
            .Select(o => new
            {
                o.CreationTime,
                o.ShippingFee,
                OrderLines = o.OrderLines.Select(ol => new
                {
                    ol.Quantity,
                    ol.RecommendedRetailPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        // Group and calculate revenue in memory to avoid EF translation issues
        var revenueByPeriod = groupBy.ToLower() switch
        {
            "day" => orders
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice)),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToList(),

            "week" => orders
                .GroupBy(o => GetWeekStartDate(o.CreationTime!.Value))
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice)),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToList(),

            "month" => orders
                .GroupBy(o => new DateTime(o.CreationTime!.Value.Year, o.CreationTime!.Value.Month, 1))
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice)),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToList(),

            _ => orders
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.ShippingFee + o.OrderLines.Sum(ol => ol.Quantity * ol.RecommendedRetailPrice)),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToList()
        };

        return revenueByPeriod;
    }

    private static DateTime GetWeekStartDate(DateTimeOffset date)
    {
        var year = date.Year;
        var dayOfYear = date.DayOfYear;
        var weekNumber = (dayOfYear - 1) / 7;
        return new DateTime(year, 1, 1).AddDays(weekNumber * 7);
    }

    private static double CalculateGrowthPercentage(decimal current, decimal previous)
    {
        if (previous == 0) return current > 0 ? 100 : 0;
        return (double)((current - previous) / previous * 100);
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
            "day" => (now.Date, now),
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
