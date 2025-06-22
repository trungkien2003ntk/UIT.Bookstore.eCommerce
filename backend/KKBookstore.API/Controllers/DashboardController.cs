using KKBookstore.Abstractions;
using KKBookstore.Features.Dashboard.GetDashboardSummary;
using KKBookstore.Features.Dashboard.GetRevenueAnalytics;
using KKBookstore.Features.Dashboard.GetCustomerAnalytics;
using KKBookstore.Features.Dashboard.GetInventoryAnalytics;
using KKBookstore.Features.Dashboard.GetOrderAnalytics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/dashboard")]
[Authorize] // Ensure only authenticated users can access dashboard
public class DashboardController(
    ISender sender
) : ApiController(sender)
{
    /// <summary>
    /// Get comprehensive dashboard summary including orders, users, stock, sales by product types, and top products
    /// </summary>
    [HttpGet("summary")]
    public async Task<IActionResult> GetDashboardSummaryAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        [FromQuery] List<int>? productTypeIds = null,
        [FromQuery] int topProductsLimit = 5,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDashboardSummaryQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            ProductTypeIds = productTypeIds ?? new List<int>(),
            TopProductsLimit = topProductsLimit
        };

        var result = await Sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get detailed revenue analytics with growth comparison and revenue trends
    /// </summary>
    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenueAnalyticsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] string groupBy = "day",
        [FromQuery] int? branchId = null,
        [FromQuery] List<int>? productTypeIds = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRevenueAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            GroupBy = groupBy,
            BranchId = branchId,
            ProductTypeIds = productTypeIds ?? new List<int>()
        };

        var result = await Sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get customer analytics including acquisition trends and retention metrics
    /// </summary>
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomerAnalyticsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] string groupBy = "day",
        [FromQuery] int? branchId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCustomerAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            GroupBy = groupBy,
            BranchId = branchId
        };

        var result = await Sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get inventory analytics including stock levels and low stock alerts
    /// </summary>
    [HttpGet("inventory")]
    public async Task<IActionResult> GetInventoryAnalyticsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        [FromQuery] List<int>? productTypeIds = null,
        [FromQuery] int lowStockThreshold = 10,
        [FromQuery] int lowStockProductsLimit = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetInventoryAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            ProductTypeIds = productTypeIds ?? new List<int>(),
            LowStockThreshold = lowStockThreshold,
            LowStockProductsLimit = lowStockProductsLimit
        };

        var result = await Sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get order analytics including volume trends and status distribution
    /// </summary>
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrderAnalyticsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        [FromQuery] List<string>? orderStatuses = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetOrderAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            OrderStatuses = orderStatuses ?? new List<string>()
        };

        var result = await Sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get top products by sales volume (Alternative endpoint for more flexible filtering)
    /// </summary>
    [HttpGet("top-products")]
    public async Task<IActionResult> GetTopProductsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        [FromQuery] List<int>? productTypeIds = null,
        [FromQuery] int limit = 5,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDashboardSummaryQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            ProductTypeIds = productTypeIds ?? new List<int>(),
            TopProductsLimit = limit
        };

        var result = await Sender.Send(query, cancellationToken);
        
        // Return only the top products from the summary
        return result.IsSuccess ? Ok(result.Value.TopProducts) : ToActionResult(result);
    }

    /// <summary>
    /// Get sales breakdown by product types
    /// </summary>
    [HttpGet("sales-by-product-types")]
    public async Task<IActionResult> GetSalesByProductTypesAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        [FromQuery] List<int>? productTypeIds = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDashboardSummaryQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            ProductTypeIds = productTypeIds ?? new List<int>(),
            TopProductsLimit = 5 // Not used for this endpoint
        };

        var result = await Sender.Send(query, cancellationToken);
        
        // Return only the sales by product types from the summary
        return result.IsSuccess ? Ok(result.Value.SalesByProductTypes) : ToActionResult(result);
    }

    /// <summary>
    /// Get comprehensive KPIs (Key Performance Indicators)
    /// </summary>
    [HttpGet("kpis")]
    public async Task<IActionResult> GetKPIsAsync(
        [FromQuery] DateTimeOffset? fromDate,
        [FromQuery] DateTimeOffset? toDate,
        [FromQuery] string period = "month",
        [FromQuery] int? branchId = null,
        CancellationToken cancellationToken = default)
    {
        // Get multiple analytics data
        var summaryTask = Sender.Send(new GetDashboardSummaryQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            ProductTypeIds = new List<int>(),
            TopProductsLimit = 5
        }, cancellationToken);

        var revenueTask = Sender.Send(new GetRevenueAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            GroupBy = "day",
            BranchId = branchId,
            ProductTypeIds = new List<int>()
        }, cancellationToken);

        var customerTask = Sender.Send(new GetCustomerAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            GroupBy = "day",
            BranchId = branchId
        }, cancellationToken);

        var orderTask = Sender.Send(new GetOrderAnalyticsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Period = period,
            BranchId = branchId,
            OrderStatuses = new List<string>()
        }, cancellationToken);

        await Task.WhenAll(summaryTask, revenueTask, customerTask, orderTask);

        var summaryResult = await summaryTask;
        var revenueResult = await revenueTask;
        var customerResult = await customerTask;
        var orderResult = await orderTask;

        if (!summaryResult.IsSuccess)
            return ToActionResult(summaryResult);
        if (!revenueResult.IsSuccess)
            return ToActionResult(revenueResult);
        if (!customerResult.IsSuccess)
            return ToActionResult(customerResult);
        if (!orderResult.IsSuccess)
            return ToActionResult(orderResult);

        var kpis = new
        {
            // Financial KPIs
            TotalRevenue = summaryResult.Value.TotalRevenue,
            AverageOrderValue = summaryResult.Value.AverageOrderValue,
            RevenueGrowth = revenueResult.Value.GrowthPercentage,
            
            // Order KPIs
            TotalOrders = summaryResult.Value.TotalOrders,
            OrderFulfillmentRate = orderResult.Value.OrderFulfillmentRate,
            AverageProcessingTime = orderResult.Value.AverageProcessingTime,
            
            // Customer KPIs
            TotalNewUsers = summaryResult.Value.TotalNewUsers,
            CustomerRetentionRate = customerResult.Value.CustomerRetentionRate,
            AverageCustomerValue = customerResult.Value.AverageCustomerValue,
            
            // Product KPIs
            TotalProductsSold = summaryResult.Value.TotalProductsSold,
            StockAdjustmentOrders = summaryResult.Value.TotalStockAdjustmentOrders
        };

        return Ok(kpis);
    }
}
