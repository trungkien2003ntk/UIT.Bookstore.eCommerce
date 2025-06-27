using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Features.Products.GetTrendyProductList.GetTrendyProductListResponse;

namespace KKBookstore.Features.Products.GetTrendyProductList;

public record GetTrendyProductListQuery(int? ProductTypeId = default) : IRequest<Result<GetTrendyProductListResponse>>;

// todo: considering merge this query with GetProductListQuery
public class GetTrendyProductListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetTrendyProductListQuery, Result<GetTrendyProductListResponse>>
{
    public async Task<Result<GetTrendyProductListResponse>> Handle(GetTrendyProductListQuery request, CancellationToken cancellationToken)
    {
        var numberOfTrendyProducts = 12;

        // Get the most purchased product IDs with their sold counts
        var topMostPurchasedProductsQuery = dbContext.OrderLines
            .AsSplitQuery()
            .Include(ol => ol.ProductVariant)
                .ThenInclude(pv => pv.Product)
            .Where(ol => ol.ProductVariant.Product.IsActive)
            .GroupBy(ol => ol.ProductVariant.ProductId)
            .Select(g => new { ProductId = g.Key, SoldCount = g.Count() })
            .OrderByDescending(x => x.SoldCount)
            .Take(numberOfTrendyProducts);

        // Apply ProductTypeId filter if provided
        if (request.ProductTypeId.HasValue)
        {
            topMostPurchasedProductsQuery = dbContext.OrderLines
                .Include(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .Where(ol => ol.ProductVariant.Product.IsActive &&
                           ol.ProductVariant.Product.ProductTypeId == request.ProductTypeId.Value)
                .GroupBy(ol => ol.ProductVariant.ProductId)
                .Select(g => new { ProductId = g.Key, SoldCount = g.Count() })
                .OrderByDescending(x => x.SoldCount)
                .Take(numberOfTrendyProducts);
        }

        var topMostPurchasedProducts = await topMostPurchasedProductsQuery.ToListAsync(cancellationToken);
        var topMostPurchasedProductIds = topMostPurchasedProducts.Select(x => x.ProductId).ToList();

        // Create a dictionary for quick lookup of sold counts
        var soldCountLookup = topMostPurchasedProducts.ToDictionary(x => x.ProductId, x => x.SoldCount);

        var productGeneralQueryable = dbContext.Products
            .AsSplitQuery()
            .Where(p => topMostPurchasedProductIds.Contains(p.Id))
            .Include(p => p.ProductImages)
            .Include(p => p.ProductType)
            .Include(p => p.Ratings)
            .Include(p => p.ProductVariants)
                .ThenInclude(p => p.Inventories)
            .Select(p => new ProductSummary
            {
                Id = p.Id,
                Name = p.Name,
                ProductTypeId = p.ProductTypeId,
                ProductTypeName = p.ProductType.DisplayName,
                IsBook = p.IsBook,
                SoldCount = 0, // Will be populated from the lookup dictionary
                MinUnitPrice = p.ProductVariants.Min(s => s.UnitPrice),
                MinRecommendedRetailPrice = p.ProductVariants.Min(s => s.RecommendedRetailPrice),
                AverageRating = Convert.ToDecimal(p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0),
                IsActive = p.IsActive,
                ThumbnailImageUrl = p.GetFirstThumbnailImageUrl(),
                TotalStockQuantity = 0 // Will be calculated after materialization
            })
            .AsQueryable();

        var productGeneralDtos = await productGeneralQueryable.ToListAsync(cancellationToken);

        // Calculate TotalStockQuantity separately to avoid nested aggregation
        var productIds = productGeneralDtos.Select(p => p.Id).ToList();
        var stockQuantities = await dbContext.Products
            .AsSplitQuery()
            .Where(p => productIds.Contains(p.Id))
            .SelectMany(p => p.ProductVariants)
            .SelectMany(v => v.Inventories!)
            .Where(i => i.IsActive)
            .GroupBy(i => i.ProductVariant.ProductId)
            .Select(g => new { ProductId = g.Key, TotalStock = g.Sum(i => i.StockQuantity) })
            .ToListAsync(cancellationToken);

        var stockLookup = stockQuantities.ToDictionary(x => x.ProductId, x => x.TotalStock);

        // Populate SoldCount and TotalStockQuantity from the lookup dictionaries
        foreach (var product in productGeneralDtos)
        {
            if (soldCountLookup.TryGetValue(product.Id, out var soldCount))
            {
                product.SoldCount = soldCount;
            }

            if (stockLookup.TryGetValue(product.Id, out var totalStock))
            {
                product.TotalStockQuantity = totalStock;
            }
        }

        // Sort by sold count (descending order for trendy products)
        productGeneralDtos.Sort((x, y) => y.SoldCount.CompareTo(x.SoldCount));

        var response = new GetTrendyProductListResponse
        {
            Items = productGeneralDtos
        };

        return response;
    }
}