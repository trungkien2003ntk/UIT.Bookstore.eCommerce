using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetOrderAnalytics;

public class GetOrderAnalyticsHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetOrderAnalyticsQuery, Result<OrderAnalyticsDto>>
{
    public async Task<Result<OrderAnalyticsDto>> Handle(GetOrderAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFilter = GetDateFilter(request.Period, request.FromDate, request.ToDate);

            // Base query
            var ordersQuery = dbContext.Orders
                .Where(o => o.CreationTime >= dateFilter.FromDate && o.CreationTime <= dateFilter.ToDate);            // Apply filters
            // Note: Orders don't have a direct BranchId - commenting out for now
            // if (request.BranchId.HasValue)
            // {
            //     ordersQuery = ordersQuery.Where(o => o.BranchId == request.BranchId.Value);
            // }

            if (request.OrderStatuses.Any())
            {
                var statusEnums = request.OrderStatuses
                    .Select(s => Enum.Parse<OrderStatus>(s, true))
                    .ToList();
                ordersQuery = ordersQuery.Where(o => statusEnums.Contains(o.Status));
            }

            // Calculate metrics
            var totalOrdersTask = ordersQuery.CountAsync(cancellationToken);
            var pendingOrdersTask = ordersQuery.CountAsync(o => o.Status == OrderStatus.Pending, cancellationToken);
            var processingOrdersTask = ordersQuery.CountAsync(o => o.Status == OrderStatus.Processing, cancellationToken);
            var shippedOrdersTask = ordersQuery.CountAsync(o => o.Status == OrderStatus.Shipped, cancellationToken);
            var deliveredOrdersTask = ordersQuery.CountAsync(o => o.Status == OrderStatus.Delivered, cancellationToken);
            var cancelledOrdersTask = ordersQuery.CountAsync(o => o.Status == OrderStatus.Cancelled, cancellationToken);

            // Calculate fulfillment rate and processing time
            var fulfillmentRateTask = CalculateOrderFulfillmentRate(ordersQuery, cancellationToken);
            var avgProcessingTimeTask = CalculateAverageProcessingTime(ordersQuery, cancellationToken);
            var orderStatusDistributionTask = GetOrderStatusDistribution(ordersQuery, cancellationToken);

            await Task.WhenAll(
                totalOrdersTask, pendingOrdersTask, processingOrdersTask, shippedOrdersTask,
                deliveredOrdersTask, cancelledOrdersTask, fulfillmentRateTask, avgProcessingTimeTask,
                orderStatusDistributionTask
            );

            var result = new OrderAnalyticsDto
            {
                TotalOrders = await totalOrdersTask,
                PendingOrders = await pendingOrdersTask,
                ProcessingOrders = await processingOrdersTask,
                ShippedOrders = await shippedOrdersTask,
                DeliveredOrders = await deliveredOrdersTask,
                CancelledOrders = await cancelledOrdersTask,
                OrderFulfillmentRate = await fulfillmentRateTask,
                AverageProcessingTime = await avgProcessingTimeTask,
                OrderStatusDistribution = await orderStatusDistributionTask
            };

            return Result<OrderAnalyticsDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.OrderAnalyticsError", $"Error retrieving order analytics: {ex.Message}");
            return Result.Failure<OrderAnalyticsDto>(error);
        }
    }

    private async Task<double> CalculateOrderFulfillmentRate(IQueryable<Order> ordersQuery, CancellationToken cancellationToken)
    {
        var totalOrders = await ordersQuery.CountAsync(cancellationToken);
        if (totalOrders == 0) return 0;

        var fulfilledOrders = await ordersQuery
            .CountAsync(o => o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received, cancellationToken);

        return (double)fulfilledOrders / totalOrders * 100;
    }

    private async Task<double> CalculateAverageProcessingTime(IQueryable<Order> ordersQuery, CancellationToken cancellationToken)
    {
        var processedOrders = await ordersQuery
            .Where(o => o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Received)
            .Where(o => o.PaidWhen.HasValue)
            .Select(o => new
            {
                OrderWhen = o.OrderWhen,
                DeliveredWhen = o.Status == OrderStatus.Delivered ? o.ConfirmedDeliveryWhen : o.ConfirmedReceivedWhen
            })
            .ToListAsync(cancellationToken);

        if (!processedOrders.Any()) return 0;

        var processingTimes = processedOrders
            .Where(o => o.DeliveredWhen.HasValue)
            .Select(o => (o.DeliveredWhen!.Value - o.OrderWhen).TotalHours)
            .ToList();

        return processingTimes.Average();
    }

    private async Task<List<OrderStatusDistributionDto>> GetOrderStatusDistribution(
        IQueryable<Order> ordersQuery,
        CancellationToken cancellationToken)
    {
        var totalOrders = await ordersQuery.CountAsync(cancellationToken);
        if (totalOrders == 0) return [];

        var statusDistribution = await ordersQuery
            .GroupBy(o => o.Status)
            .Select(g => new OrderStatusDistributionDto
            {
                Status = g.Key.ToString(),
                Count = g.Count(),
                Percentage = 0 // Will be calculated below
            })
            .ToListAsync(cancellationToken);

        // Calculate percentages
        foreach (var status in statusDistribution)
        {
            status.Percentage = (double)status.Count / totalOrders * 100;
        }

        return statusDistribution.OrderByDescending(s => s.Count).ToList();
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
}
