using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetInventoryAnalytics;

public class GetInventoryAnalyticsHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetInventoryAnalyticsQuery, Result<InventoryAnalyticsDto>>
{
    public async Task<Result<InventoryAnalyticsDto>> Handle(GetInventoryAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFilter = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            // Base queries
            var productsQuery = dbContext.Products.Where(p => !p.IsDeleted && p.IsActive);
            var inventoryQuery = dbContext.Inventories.AsQueryable();
            var stockAdjustmentsQuery = dbContext.StockAdjustments
                .Where(sa => sa.CreationTime >= dateFilter.FromDate && sa.CreationTime <= dateFilter.ToDate);

            // Apply filters
            if (request.BranchId.HasValue)
            {
                // Filter inventory by warehouse (branch)
                inventoryQuery = inventoryQuery.Where(i => i.WarehouseId == request.BranchId.Value);
                stockAdjustmentsQuery = stockAdjustmentsQuery.Where(sa => sa.WarehouseId == request.BranchId.Value);
            }

            if (request.ProductTypeIds.Any())
            {
                productsQuery = productsQuery.Where(p => request.ProductTypeIds.Contains(p.ProductTypeId));
            }            // Execute queries sequentially to avoid DbContext connection issues
            var totalProducts = await productsQuery.CountAsync(cancellationToken);

            var lowStockProducts = await GetLowStockProducts(
                productsQuery,
                inventoryQuery,
                request.LowStockThreshold,
                request.LowStockProductsLimit,
                cancellationToken);
                
            var outOfStockProducts = await inventoryQuery
                .Where(i => i.StockQuantity <= 0)
                .CountAsync(cancellationToken);

            var totalStockAdjustments = await stockAdjustmentsQuery.CountAsync(cancellationToken);

            var result = new InventoryAnalyticsDto
            {
                TotalProducts = totalProducts,
                LowStockProducts = lowStockProducts.Count,
                OutOfStockProducts = outOfStockProducts,
                TotalStockTransactions = totalStockAdjustments,
                LowStockProductsList = lowStockProducts
            };

            return Result<InventoryAnalyticsDto>.Success(result);
        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.InventoryAnalyticsError", $"Error retrieving inventory analytics: {ex.Message}");
            return Result.Failure<InventoryAnalyticsDto>(error);
        }
    }
    private async Task<List<LowStockProductDto>> GetLowStockProducts(
        IQueryable<Product> productsQuery,
        IQueryable<StockTransactions.Inventory> inventoryQuery,
        int lowStockThreshold,
        int limit,
        CancellationToken cancellationToken)
    {
        var lowStockProducts = await (from p in productsQuery
                                      join pv in dbContext.ProductVariants on p.Id equals pv.ProductId
                                      join i in inventoryQuery on pv.Id equals i.ProductVariantId
                                      where i.StockQuantity <= lowStockThreshold && i.StockQuantity > 0
                                      select new LowStockProductDto
                                      {
                                          ProductId = p.Id,
                                          ProductName = p.Name,
                                          CurrentStock = i.StockQuantity,
                                          MinimumStock = lowStockThreshold,
                                          ProductTypeName = p.ProductType.DisplayName
                                      })
                                     .OrderBy(p => p.CurrentStock)
                                     .Take(limit)
                                     .ToListAsync(cancellationToken);

        return lowStockProducts;
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
