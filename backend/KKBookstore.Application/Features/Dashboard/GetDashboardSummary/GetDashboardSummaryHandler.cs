using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetDashboardSummary;

public class GetDashboardSummaryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetDashboardSummaryQuery, Result<DashboardSummaryDto>>
{
    public async Task<Result<DashboardSummaryDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var currentPeriod = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            var previousPeriod = GetPreviousPeriodFilter(request.Period, request.FromDate, request.ToDate);

            // Get current period metrics
            var currentMetrics = await GetPeriodMetrics(currentPeriod, cancellationToken);

            // Get previous period metrics
            var previousMetrics = await GetPeriodMetrics(previousPeriod, cancellationToken);

            // Calculate percentage changes
            var result = new DashboardSummaryDto
            {
                TotalOrders = currentMetrics.TotalOrders,
                TotalOrdersChangePercent = CalculatePercentageChange(previousMetrics.TotalOrders, currentMetrics.TotalOrders),

                TotalNewUsers = currentMetrics.TotalNewUsers,
                TotalNewUsersChangePercent = CalculatePercentageChange(previousMetrics.TotalNewUsers, currentMetrics.TotalNewUsers),

                TotalRevenue = currentMetrics.TotalRevenue,
                TotalRevenueChangePercent = CalculatePercentageChange(previousMetrics.TotalRevenue, currentMetrics.TotalRevenue),

                AverageOrderValue = currentMetrics.AverageOrderValue,
                AverageOrderValueChangePercent = CalculatePercentageChange(previousMetrics.AverageOrderValue, currentMetrics.AverageOrderValue),

                TotalProductsSold = currentMetrics.TotalProductsSold,
                TotalProductsSoldChangePercent = CalculatePercentageChange(previousMetrics.TotalProductsSold, currentMetrics.TotalProductsSold),

                TotalProfit = currentMetrics.TotalProfit,
                TotalProfitChangePercent = CalculatePercentageChange(previousMetrics.TotalProfit, currentMetrics.TotalProfit),

                ProfitMargin = currentMetrics.ProfitMargin,
                ProfitMarginChangePercent = CalculatePercentageChange(previousMetrics.ProfitMargin, currentMetrics.ProfitMargin),

                TotalStockAdjustmentOrders = 0, // TODO: Implement when stock transactions are available
                TotalStockAdjustmentOrdersChangePercent = 0,

                TopProducts = await GetTopProductsWithComparison(currentPeriod, previousPeriod, cancellationToken),
                SalesByProductTypes = await GetSalesByProductTypesWithComparison(currentPeriod, previousPeriod, cancellationToken)
            };

            return Result<DashboardSummaryDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.SummaryError", $"Error retrieving dashboard summary: {ex.Message}");
            return Result.Failure<DashboardSummaryDto>(error);
        }
    }

    private async Task<PeriodMetrics> GetPeriodMetrics(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) period,
        CancellationToken cancellationToken)
    {
        // Get total orders for period
        var ordersQuery = dbContext.Orders.AsQueryable();
        if (period.FromDate.HasValue)
            ordersQuery = ordersQuery.Where(o => o.OrderWhen >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            ordersQuery = ordersQuery.Where(o => o.OrderWhen <= period.ToDate.Value);

        var totalOrders = await ordersQuery.CountAsync(cancellationToken);

        // Get total new users for period
        var usersQuery = dbContext.Users.AsQueryable();
        if (period.FromDate.HasValue)
            usersQuery = usersQuery.Where(u => u.CreationTime >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            usersQuery = usersQuery.Where(u => u.CreationTime <= period.ToDate.Value);

        var totalNewUsers = await usersQuery.CountAsync(cancellationToken);

        // Get total revenue, products sold, and profit for period
        var orderLinesQuery = dbContext.OrderLines
            .Include(ol => ol.Order)
            .Include(ol => ol.ProductVariant)
            .Where(ol => ol.Order.Status == OrderStatus.Delivered || ol.Order.Status == OrderStatus.Received);

        if (period.FromDate.HasValue)
            orderLinesQuery = orderLinesQuery.Where(ol => ol.Order.OrderWhen >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            orderLinesQuery = orderLinesQuery.Where(ol => ol.Order.OrderWhen <= period.ToDate.Value);

        var revenueAndQuantity = await orderLinesQuery
            .GroupBy(ol => 1) // Group all records together
            .Select(g => new
            {
                TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice), // Fixed: Use actual selling price (UnitPrice) not RecommendedRetailPrice
                TotalQuantity = g.Sum(ol => ol.Quantity),
                TotalCost = g.Sum(ol => ol.Quantity * ol.ProductVariant.UnitPrice), // Cost basis for profit calculation
                TotalProfit = g.Sum(ol => ol.Quantity * (ol.UnitPrice - ol.ProductVariant.UnitPrice)) // Profit = (Selling Price - Cost) * Quantity
            })
            .FirstOrDefaultAsync(cancellationToken);

        var totalRevenue = revenueAndQuantity?.TotalRevenue ?? 0;
        var totalProductsSold = revenueAndQuantity?.TotalQuantity ?? 0;
        var totalProfit = revenueAndQuantity?.TotalProfit ?? 0;
        var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;
        var profitMargin = totalRevenue > 0 ? (totalProfit / totalRevenue) * 100 : 0;

        return new PeriodMetrics
        {
            TotalOrders = totalOrders,
            TotalNewUsers = totalNewUsers,
            TotalRevenue = totalRevenue,
            TotalProductsSold = totalProductsSold,
            AverageOrderValue = averageOrderValue,
            TotalProfit = totalProfit,
            ProfitMargin = profitMargin
        };
    }

    private async Task<List<TopProductDto>> GetTopProductsWithComparison(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) currentPeriod,
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) previousPeriod,
        CancellationToken cancellationToken)
    {
        // Get current period top products
        var currentProducts = await GetTopProductsForPeriod(currentPeriod, cancellationToken);

        // Get previous period data for comparison
        var previousProductsData = await GetProductsDataForPeriod(previousPeriod, cancellationToken);
        var previousProductsDict = previousProductsData.ToDictionary(p => p.ProductId, p => p.TotalQuantitySold);

        // Add percentage changes
        foreach (var product in currentProducts)
        {
            var previousQuantity = previousProductsDict.GetValueOrDefault(product.ProductId, 0);
            product.QuantityChangePercent = CalculatePercentageChange(previousQuantity, product.TotalQuantitySold);
        }

        return currentProducts;
    }

    private async Task<List<TopProductDto>> GetTopProductsForPeriod(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) period,
        CancellationToken cancellationToken)
    {
        var query = dbContext.OrderLines
            .Include(ol => ol.Order)
            .Include(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Product)
                    .ThenInclude(p => p.ProductType)
            .Where(ol => ol.Order.Status == OrderStatus.Delivered || ol.Order.Status == OrderStatus.Received);

        if (period.FromDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen <= period.ToDate.Value);

        return await query
            .GroupBy(ol => new { ol.ProductVariant.Product.Id, ol.ProductVariant.Product.Name })
            .Select(g => new TopProductDto
            {
                ProductId = g.Key.Id,
                ProductName = g.Key.Name,
                TotalQuantitySold = g.Sum(ol => ol.Quantity),
                TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice)
            })
            .OrderByDescending(tp => tp.TotalQuantitySold)
            .Take(5)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<TopProductDto>> GetProductsDataForPeriod(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) period,
        CancellationToken cancellationToken)
    {
        var query = dbContext.OrderLines
            .Include(ol => ol.Order)
            .Include(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Product)
            .Where(ol => ol.Order.Status == OrderStatus.Delivered || ol.Order.Status == OrderStatus.Received);

        if (period.FromDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen <= period.ToDate.Value);

        return await query
            .GroupBy(ol => new
            {
                ol.ProductVariant.Product.Id,
                ol.ProductVariant.Product.Name,
                ol.ProductVariant.Product.ProductType.DisplayName
            }).Select(g => new TopProductDto
            {
                ProductId = g.Key.Id,
                ProductName = g.Key.Name,
                ProductTypeName = g.Key.DisplayName,
                ProductImageUrl = g.Select(ol => ol.ProductVariant.Product.ProductImages.FirstOrDefault().ThumbnailImageUrl).FirstOrDefault(),
                TotalQuantitySold = g.Sum(ol => ol.Quantity),
                TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice)
            })
            .ToListAsync(cancellationToken);
    }

    private async Task<List<SalesByProductTypeDto>> GetSalesByProductTypesWithComparison(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) currentPeriod,
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) previousPeriod,
        CancellationToken cancellationToken)
    {
        // Get current period data
        var currentData = await GetSalesByProductTypesForPeriod(currentPeriod, cancellationToken);

        // Get previous period data
        var previousData = await GetSalesByProductTypesForPeriod(previousPeriod, cancellationToken);
        var previousDataDict = previousData.ToDictionary(p => p.ProductTypeId, p => p.TotalRevenue);

        // Add percentage changes
        foreach (var item in currentData)
        {
            var previousRevenue = previousDataDict.GetValueOrDefault(item.ProductTypeId, 0);
            item.RevenueChangePercent = CalculatePercentageChange(previousRevenue, item.TotalRevenue);
        }

        return currentData;
    }

    private async Task<List<SalesByProductTypeDto>> GetSalesByProductTypesForPeriod(
        (DateTimeOffset? FromDate, DateTimeOffset? ToDate) period,
        CancellationToken cancellationToken)
    {
        var query = dbContext.OrderLines
            .Include(ol => ol.Order)
            .Include(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Product)
                    .ThenInclude(p => p.ProductType)
            .Where(ol => ol.Order.Status == OrderStatus.Delivered || ol.Order.Status == OrderStatus.Received);

        if (period.FromDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen >= period.FromDate.Value);
        if (period.ToDate.HasValue)
            query = query.Where(ol => ol.Order.OrderWhen <= period.ToDate.Value);

        return await query
            .GroupBy(ol => new
            {
                ol.ProductVariant.Product.ProductType.Id,
                ol.ProductVariant.Product.ProductType.DisplayName
            })
            .Select(g => new SalesByProductTypeDto
            {
                ProductTypeId = g.Key.Id,
                ProductTypeName = g.Key.DisplayName,
                TotalQuantitySold = g.Sum(ol => ol.Quantity),
                TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice)
            })
            .OrderByDescending(s => s.TotalRevenue)
            .ToListAsync(cancellationToken);
    }

    private decimal CalculatePercentageChange(decimal previousValue, decimal currentValue)
    {
        if (previousValue == 0)
            return currentValue > 0 ? 100 : 0;

        return Math.Round(((currentValue - previousValue) / previousValue) * 100, 2);
    }

    private (DateTimeOffset? FromDate, DateTimeOffset? ToDate) GetDateFilter(string period, DateTimeOffset? fromDate, DateTimeOffset? toDate)
    {
        if (fromDate.HasValue && toDate.HasValue)
            return (fromDate, toDate);

        var now = DateTimeOffset.Now;
        return period?.ToLower() switch
        {
            "week" => (now.AddDays(-7), now),
            "month" => (now.AddMonths(-1), now),
            "year" => (now.AddYears(-1), now),
            _ => (fromDate, toDate)
        };
    }

    private (DateTimeOffset? FromDate, DateTimeOffset? ToDate) GetPreviousPeriodFilter(string period, DateTimeOffset? fromDate, DateTimeOffset? toDate)
    {
        if (fromDate.HasValue && toDate.HasValue)
        {
            var periodLength = toDate.Value - fromDate.Value;
            return (fromDate.Value - periodLength, fromDate.Value);
        }

        var now = DateTimeOffset.Now;
        return period?.ToLower() switch
        {
            "week" => (now.AddDays(-14), now.AddDays(-7)),
            "month" => (now.AddMonths(-2), now.AddMonths(-1)),
            "year" => (now.AddYears(-2), now.AddYears(-1)),
            _ => (null, null)
        };
    }

    private class PeriodMetrics
    {
        public int TotalOrders { get; set; }
        public int TotalNewUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalProductsSold { get; set; }
        public decimal AverageOrderValue { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal ProfitMargin { get; set; }
    }
}