using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Features.Products.GetWeeklyTopSellingProductList.GetWeeklyTopSellingProductListResponse;

namespace KKBookstore.Features.Products.GetWeeklyTopSellingProductList;

public record GetWeeklyTopSellingProductListQuery(
    int? ProductTypeId = default,
    int? Limit = default
) : IRequest<Result<GetWeeklyTopSellingProductListResponse>>;

public class GetWeeklyTopSellingProductListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetWeeklyTopSellingProductListQuery, Result<GetWeeklyTopSellingProductListResponse>>
{
    public async Task<Result<GetWeeklyTopSellingProductListResponse>> Handle(GetWeeklyTopSellingProductListQuery request, CancellationToken cancellationToken)
    {
        // Default number of products to return if not specified
        var numberOfProducts = request.Limit ?? 12;

        // Calculate the date one week ago
        var oneWeekAgo = DateTime.Now.AddDays(-7);

        // Query to get order lines from the past week
        var recentOrderLines = dbContext.OrderLines
            .Include(ol => ol.ProductVariant)
            .Include(ol => ol.Order)
            .Where(ol => ol.Order.OrderWhen >= oneWeekAgo)
            .AsQueryable();

        // If ProductTypeId is provided, filter by it
        if (request.ProductTypeId.HasValue)
        {
            recentOrderLines = recentOrderLines
                .Include(ol => ol.ProductVariant.Product)
                .Where(ol => ol.ProductVariant.Product.ProductTypeId == request.ProductTypeId.Value);
        }

        // Get the IDs of products sold in the past week
        var recentlyBoughtProductIds = await recentOrderLines
            .Select(ol => ol.ProductVariant.ProductId)
            .ToListAsync(cancellationToken);

        // Group by product ID and count occurrences to find the most purchased products
        var topSellingProductIds = recentlyBoughtProductIds
            .GroupBy(id => id)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(numberOfProducts)
            .ToList();

        // If no products were sold in the past week, return an empty list
        if (!topSellingProductIds.Any())
        {
            return new GetWeeklyTopSellingProductListResponse
            {
                Items = new List<ProductSummary>()
            };
        }

        // Query to get detailed information about the top-selling products
        var productQueryable = dbContext.Products
            .Where(p => topSellingProductIds.Contains(p.Id))
            .Include(p => p.ProductImages)
            .Include(p => p.ProductType)
            .Include(p => p.Ratings)
            .Include(p => p.ProductVariants)
            .Select(p =>
                new ProductSummary
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProductTypeId = p.ProductTypeId,
                    ProductTypeName = p.ProductType.DisplayName,
                    IsBook = p.IsBook,
                    SoldCount = dbContext.OrderLines
                        .Include(ol => ol.ProductVariant)
                        .Count(ol => ol.ProductVariant.ProductId == p.Id && ol.Order.OrderWhen >= oneWeekAgo),
                    MinUnitPrice = p.ProductVariants.Min(s => s.UnitPrice),
                    MinRecommendedRetailPrice = p.ProductVariants.Min(s => s.RecommendedRetailPrice),
                    AverageRating = Convert.ToDecimal(p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0),
                    IsActive = p.IsActive,
                    ThumbnailImageUrl = p.GetFirstThumbnailImageUrl()
                })
            .AsQueryable();

        var productDtos = await productQueryable.ToListAsync(cancellationToken);

        // Sort products by weekly sold count in descending order
        static int comparison(ProductSummary y, ProductSummary x) => x.SoldCount.CompareTo(y.SoldCount);
        productDtos.Sort(comparison);

        var response = new GetWeeklyTopSellingProductListResponse
        {
            Items = productDtos
        };

        return response;
    }
}