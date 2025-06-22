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
{
    public async Task<Result<RevenueAnalyticsDto>> Handle(GetRevenueAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFilter = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            var previousPeriodFilter = GetPreviousPeriodFilter(dateFilter.FromDate, dateFilter.ToDate);
            
            // Base query for current period
            var currentPeriodQuery = dbContext.Orders
                .Where(o => o.CreationTime >= dateFilter.FromDate && 
                           o.CreationTime <= dateFilter.ToDate &&
                           (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received));

            // Base query for previous period
            var previousPeriodQuery = dbContext.Orders
                .Where(o => o.CreationTime >= previousPeriodFilter.FromDate && 
                           o.CreationTime <= previousPeriodFilter.ToDate &&
                           (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received));            // Apply filters
            // Note: Orders don't have a direct BranchId - commenting out for now
            // if (request.BranchId.HasValue)
            // {
            //     currentPeriodQuery = currentPeriodQuery.Where(o => o.BranchId == request.BranchId.Value);
            //     previousPeriodQuery = previousPeriodQuery.Where(o => o.BranchId == request.BranchId.Value);
            // }

            if (request.ProductTypeIds.Any())
            {
                currentPeriodQuery = currentPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
                previousPeriodQuery = previousPeriodQuery.Where(o => o.OrderLines
                    .Any(ol => request.ProductTypeIds.Contains(ol.ProductVariant.Product.ProductTypeId)));
            }

            // Calculate total revenue
            var totalRevenueTask = currentPeriodQuery
                .SumAsync(o => o.Subtotal + o.ShippingFee, cancellationToken);
            
            var previousPeriodRevenueTask = previousPeriodQuery
                .SumAsync(o => o.Subtotal + o.ShippingFee, cancellationToken);

            // Get revenue by period
            var revenueByPeriodTask = GetRevenueByPeriod(currentPeriodQuery, request.GroupBy, cancellationToken);

            await Task.WhenAll(totalRevenueTask, previousPeriodRevenueTask, revenueByPeriodTask);

            var totalRevenue = await totalRevenueTask;
            var previousPeriodRevenue = await previousPeriodRevenueTask;
            var growthPercentage = CalculateGrowthPercentage(totalRevenue, previousPeriodRevenue);

            var result = new RevenueAnalyticsDto
            {
                TotalRevenue = totalRevenue,
                PreviousPeriodRevenue = previousPeriodRevenue,
                GrowthPercentage = growthPercentage,
                RevenueByPeriod = await revenueByPeriodTask
            };

            return Result<RevenueAnalyticsDto>.Success(result);        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.GetRevenue", ex.Message);
            return Result.Failure<RevenueAnalyticsDto>(error);
        }
    }

    private async Task<List<RevenueByPeriodDto>> GetRevenueByPeriod(
        IQueryable<Order> ordersQuery, 
        string groupBy, 
        CancellationToken cancellationToken)
    {        var revenueByPeriod = groupBy.ToLower() switch
        {
            "day" => await ordersQuery
                .Where(o => o.CreationTime.HasValue)
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.Subtotal + o.ShippingFee),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToListAsync(cancellationToken),
                
            "week" => await ordersQuery
                .Where(o => o.CreationTime.HasValue)
                .GroupBy(o => new DateTime(o.CreationTime!.Value.Year, 1, 1)
                    .AddDays((o.CreationTime!.Value.DayOfYear - 1) / 7 * 7))
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.Subtotal + o.ShippingFee),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToListAsync(cancellationToken),
                
            "month" => await ordersQuery
                .Where(o => o.CreationTime.HasValue)
                .GroupBy(o => new DateTime(o.CreationTime!.Value.Year, o.CreationTime!.Value.Month, 1))
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.Subtotal + o.ShippingFee),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToListAsync(cancellationToken),
                
            _ => await ordersQuery
                .Where(o => o.CreationTime.HasValue)
                .GroupBy(o => o.CreationTime!.Value.Date)
                .Select(g => new RevenueByPeriodDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(o => o.Subtotal + o.ShippingFee),
                    OrderCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToListAsync(cancellationToken)
        };

        return revenueByPeriod;
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
