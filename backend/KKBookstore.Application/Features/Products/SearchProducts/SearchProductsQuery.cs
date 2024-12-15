using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.Products.SearchProducts.SearchProductsResponse;

namespace KKBookstore.Application.Features.Products.SearchProducts;

public record SearchProductsQuery : IRequest<Result<SearchProductsResponse>>
{
    public string SearchText { get; init; } = "*";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 12;
}

public class SearchProductHandler(
    IApplicationDbContext dbContext,
    ISearchService searchService
) : IRequestHandler<SearchProductsQuery, Result<SearchProductsResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<SearchProductsResponse>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var searchRequest = new SearchRequest
        {
            SearchText = request.SearchText,
            Page = request.Page,
            PageSize = request.PageSize
        };

        SearchResult searchResult = await searchService.SearchAsync(searchRequest);

        var productIds = searchResult.ProductIds;

        var products = _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Include(p => p.ProductType)
            .Include(p => p.Ratings)
            .Include(p => p.ProductVariants)
            .Include(p => p.ProductImages)
            .Select(p => new ProductDto
            {
                AverageRating = Convert.ToDecimal(p.Ratings.Count > 0 ? p.Ratings.Average(r => r.RatingValue) : 0),
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                IsActive = p.IsActive,
                IsBook = p.IsBook,
                MinRecommendedRetailPrice = p.ProductVariants.Min(s => s.RecommendedRetailPrice),
                MinUnitPrice = p.ProductVariants.Min(s => s.UnitPrice),
                ProductTypeId = p.ProductTypeId,
                ProductTypeName = p.ProductType.DisplayName,
                ThumbnailImageUrl = p.GetFirstThumbnailImageUrl(),
            })
            .ToList();

        var orderedProducts = products
            .OrderBy(p => productIds.IndexOf(p.Id))
            .ToList();

        var response = new SearchProductsResponse
        {
            TotalCount = searchResult.TotalCount,
            TotalPages = searchResult.TotalPages,
            PageSize = searchResult.PageSize,
            PageNumber = searchResult.PageNumber,
            Products = orderedProducts
        };

        return response;
    }
}
