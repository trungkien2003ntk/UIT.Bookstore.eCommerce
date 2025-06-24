using System.Linq.Expressions;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetProfitAnalytics;

public class GetProfitAnalyticsHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetProfitAnalyticsQuery, Result<ProfitAnalyticsDto>>
{
    public async Task<Result<ProfitAnalyticsDto>> Handle(GetProfitAnalyticsQuery request, CancellationToken cancellationToken)
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
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Inventories)
                .AsNoTracking();

            // Apply status filter - only count delivered/received orders for profit
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
                           statusFilter.Contains(o.Status));            // Apply product type filter if specified
            if (request.ProductTypeIds.Any())
            {
                currentPeriodQuery = currentPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
                previousPeriodQuery = previousPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
            }

            // Note: Branch filtering would require complex joins with OrderFulfillments
            // For now, we'll skip branch filtering in profit analytics

            // Execute queries sequentially to avoid DbContext connection issues
            var currentPeriodData = await CalculateProfitData(currentPeriodQuery, cancellationToken);
            var previousPeriodData = await CalculateProfitData(previousPeriodQuery, cancellationToken);
            var profitByPeriod = await GetProfitByPeriod(currentPeriodQuery, request.GroupBy, cancellationToken);
            
            var growthPercentage = CalculateGrowthPercentage(currentPeriodData.TotalProfit, previousPeriodData.TotalProfit);
            var profitMarginPercentage = currentPeriodData.TotalRevenue > 0 
                ? (double)(currentPeriodData.TotalProfit / currentPeriodData.TotalRevenue * 100) 
                : 0;

            var result = new ProfitAnalyticsDto
            {
                TotalProfit = currentPeriodData.TotalProfit,
                PreviousPeriodProfit = previousPeriodData.TotalProfit,
                GrowthPercentage = growthPercentage,
                TotalRevenue = currentPeriodData.TotalRevenue,
                TotalCost = currentPeriodData.TotalCost,
                ProfitMarginPercentage = profitMarginPercentage,
                ProfitByPeriod = profitByPeriod
            };

            return Result<ProfitAnalyticsDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.GetProfit", ex.Message);
            return Result.Failure<ProfitAnalyticsDto>(error);
        }
    }

    private async Task<(decimal TotalProfit, decimal TotalRevenue, decimal TotalCost)> CalculateProfitData(
        IQueryable<Order> ordersQuery, 
        CancellationToken cancellationToken)
    {
        // Get all orders with their line items and inventory data to calculate profit in memory
        // This avoids EF Core translation issues with complex aggregations
        var orders = await ordersQuery
            .Select(o => new
            {
                o.ShippingFee,
                OrderLines = o.OrderLines.Select(ol => new
                {
                    ol.Quantity,
                    ol.RecommendedRetailPrice,
                    ol.ProductVariantId,                    // Get the latest unit cost from inventories
                    LatestUnitCost = ol.ProductVariant.Inventories != null
                        ? ol.ProductVariant.Inventories
                            .Where(i => i.IsActive)
                            .OrderByDescending(i => i.CreationTime)
                            .Select(i => i.UnitCost)
                            .FirstOrDefault()
                        : 0
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        // Calculate totals in memory
        decimal totalRevenue = 0;
        decimal totalCost = 0;

        foreach (var order in orders)
        {
            // Add shipping fee to revenue
            totalRevenue += order.ShippingFee;

            foreach (var orderLine in order.OrderLines)
            {
                var lineRevenue = orderLine.Quantity * orderLine.RecommendedRetailPrice;
                var lineCost = orderLine.Quantity * orderLine.LatestUnitCost;

                totalRevenue += lineRevenue;
                totalCost += lineCost;
            }
        }

        var totalProfit = totalRevenue - totalCost;

        return (totalProfit, totalRevenue, totalCost);
    }

    private async Task<List<ProfitByPeriodDto>> GetProfitByPeriod(
        IQueryable<Order> ordersQuery,
        string groupBy,
        CancellationToken cancellationToken)
    {
        // Get orders with minimal data needed for grouping and profit calculation
        var orders = await ordersQuery
            .Where(o => o.CreationTime.HasValue)
            .Select(o => new
            {
                o.CreationTime,
                o.ShippingFee,
                OrderLines = o.OrderLines.Select(ol => new
                {
                    ol.Quantity,
                    ol.RecommendedRetailPrice,
                    ol.ProductVariantId,                    LatestUnitCost = ol.ProductVariant.Inventories != null
                        ? ol.ProductVariant.Inventories
                            .Where(i => i.IsActive)
                            .OrderByDescending(i => i.CreationTime)
                            .Select(i => i.UnitCost)
                            .FirstOrDefault()
                        : 0
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        // Group and calculate profit in memory to avoid EF translation issues
        var profitByPeriod = groupBy.ToLower() switch
        {
            "day" => orders
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => CalculatePeriodProfit(g.Key, g))
                .OrderBy(r => r.Date)
                .ToList(),

            "week" => orders
                .GroupBy(o => GetWeekStartDate(o.CreationTime!.Value))
                .Select(g => CalculatePeriodProfit(g.Key, g))
                .OrderBy(r => r.Date)
                .ToList(),

            "month" => orders
                .GroupBy(o => new DateTime(o.CreationTime!.Value.Year, o.CreationTime!.Value.Month, 1))
                .Select(g => CalculatePeriodProfit(g.Key, g))
                .OrderBy(r => r.Date)
                .ToList(),

            _ => orders
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => CalculatePeriodProfit(g.Key, g))
                .OrderBy(r => r.Date)
                .ToList()
        };

        return profitByPeriod;
    }

    private static ProfitByPeriodDto CalculatePeriodProfit(DateTime date, IEnumerable<dynamic> orders)
    {
        decimal totalRevenue = 0;
        decimal totalCost = 0;
        int orderCount = 0;

        foreach (var order in orders)
        {
            orderCount++;
            totalRevenue += order.ShippingFee;

            foreach (var orderLine in order.OrderLines)
            {
                var lineRevenue = orderLine.Quantity * orderLine.RecommendedRetailPrice;
                var lineCost = orderLine.Quantity * orderLine.LatestUnitCost;

                totalRevenue += lineRevenue;
                totalCost += lineCost;
            }
        }

        var profit = totalRevenue - totalCost;
        var profitMargin = totalRevenue > 0 ? (double)(profit / totalRevenue * 100) : 0;

        return new ProfitByPeriodDto
        {
            Date = date,
            Profit = profit,
            Revenue = totalRevenue,
            Cost = totalCost,
            ProfitMargin = profitMargin,
            OrderCount = orderCount
        };
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
