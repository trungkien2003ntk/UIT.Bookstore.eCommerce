using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.GetWeeklyTopSellingProductList;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static KKBookstore.Features.Products.GetWeeklyTopSellingProductList.GetWeeklyTopSellingProductListResponse;

namespace KKBookstore.Features.Products.GetMonthlyTopSellingProductList;

public record GetMonthlyTopSellingProductListQuery(
    int? ProductTypeId = default,
    int? Limit = default
) : IRequest<Result<GetWeeklyTopSellingProductListResponse>>;

public class GetMonthlyTopSellingProductListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetMonthlyTopSellingProductListQuery, Result<GetWeeklyTopSellingProductListResponse>>
{
    public async Task<Result<GetWeeklyTopSellingProductListResponse>> Handle(GetMonthlyTopSellingProductListQuery request, CancellationToken cancellationToken)
    {
        // Default number of products to return if not specified
        var numberOfProducts = request.Limit ?? 12;
        
        // Calculate the date one month ago
        var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
        
        // Query to get order lines from the past month
        var recentOrderLines = dbContext.OrderLines
            .Include(ol => ol.ProductVariant)
            .Include(ol => ol.Order)
            .Where(ol => ol.Order.OrderWhen >= oneMonthAgo)
            .AsQueryable();
            
        // If ProductTypeId is provided, filter by it
        if (request.ProductTypeId.HasValue)
        {
            recentOrderLines = recentOrderLines
                .Include(ol => ol.ProductVariant.Product)
                .Where(ol => ol.ProductVariant.Product.ProductTypeId == request.ProductTypeId.Value);
        }
        
        // Get the IDs of products sold in the past month
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
            
        // If no products were sold in the past month, return an empty list
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
                        .Count(ol => ol.ProductVariant.ProductId == p.Id && ol.Order.OrderWhen >= oneMonthAgo),
                    MinUnitPrice = p.ProductVariants.Min(s => s.UnitPrice),
                    MinRecommendedRetailPrice = p.ProductVariants.Min(s => s.RecommendedRetailPrice),
                    AverageRating = Convert.ToDecimal(p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0),
                    IsActive = p.IsActive,
                    ThumbnailImageUrl = p.GetFirstThumbnailImageUrl()
                })
            .AsQueryable();
            
        var productDtos = await productQueryable.ToListAsync(cancellationToken);
        
        // Sort products by monthly sold count in descending order
        static int comparison(ProductSummary y, ProductSummary x) => x.SoldCount.CompareTo(y.SoldCount);
        productDtos.Sort(comparison);
        
        var response = new GetWeeklyTopSellingProductListResponse
        {
            Items = productDtos
        };
        
        return response;
    }
}