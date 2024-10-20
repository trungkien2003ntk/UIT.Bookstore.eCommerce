using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetProductList;

public record GetProductListQuery()
    : IRequest<Result<PaginatedResult<ProductSummary>>>, ISortableQuery, IPaginatedQuery
{
    public string SortBy { get; init; }
    public string SortDirection { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    // other properties for filtering 
    public List<int> ProductTypeIds { get; set; } = [];
    public List<int> ExcludeProductIds { get; set; } = [];
    public PriceRange? PriceRange { get; set; }
    public Dictionary<string, List<string>> CustomFilters { get; set; } = [];
    public string? Search { get; set; }
}


// todo: this class is doing too much, consider refactoring
public class GetProductListQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetProductListQuery, Result<PaginatedResult<ProductSummary>>>
{
    public async Task<Result<PaginatedResult<ProductSummary>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        // Note: This query is not optimized, just need the first image in the sku images
        IQueryable<Product> query = dbContext.Products;

        query = ApplyProductIdsFilter(query, request.ProductTypeIds);
        query = ApplyPriceRangeFilter(query, request.PriceRange);
        query = ApplyExcludeProducts(query, request.ExcludeProductIds);
        var result = await ApplyCustomFiltersAsync(query, request.CustomFilters, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<PaginatedResult<ProductSummary>>(result.Error);
        }

        query = result.Value
            .Include(p => p.ProductImages)
            .Include(p => p.ProductVariants)
            .Include(p => p.ProductType)
            .Include(p => p.Ratings);


        // Apply sorting
        var validSortProperties = new List<string> { "CreatedWhen", "Name", "Id" };
        string sortProperty = request.SortBy;

        PaginatedResult<Product> paginatedProducts;

        try
        {
            paginatedProducts = await query.SortAndPaginateAsync(
                sortProperty,
                request.SortDirection,
                validSortProperties,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
        catch
        {
            return Result.Failure<PaginatedResult<ProductSummary>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        if (paginatedProducts.Items.Count == 0)
        {
            return Result.Failure<PaginatedResult<ProductSummary>>(ProductErrors.NotFound);
        }

        var mappedPaginatedProducts = mapper.Map<PaginatedResult<ProductSummary>>(paginatedProducts);

        foreach (var product in mappedPaginatedProducts.Items)
        {
            if (string.IsNullOrEmpty(product.ThumbnailImageUrl))
            {
                try
                {
                    var productInDb = query
                        .Where(p => p.Id == product.Id)
                        .Include(p => p.ProductVariants)
                            .ThenInclude(s => s.ProductVariantOptionValues)
                                .ThenInclude(s => s.OptionValue)
                        .FirstOrDefault();

                    product.ThumbnailImageUrl = productInDb!.ProductVariants.FirstOrDefault(s => !string.IsNullOrEmpty(s.GetThumbnailImageUrl()))?.GetThumbnailImageUrl();
                }
                catch
                {
                    product.ThumbnailImageUrl = "";
                }
            }
        }

        return Result.Success(mappedPaginatedProducts);

    }

    private IQueryable<Product> ApplyExcludeProducts(IQueryable<Product> query, List<int> excludeProductIds)
    {
        if (excludeProductIds.Count > 0)
        {
            query = query.Where(p => !excludeProductIds.Contains(p.Id));
        }

        return query;
    }

    private IQueryable<Product> ApplyProductIdsFilter(IQueryable<Product> query, List<int> productTypeIds)
    {
        if (productTypeIds.Count > 0)
        {
            var productTypeIdsWithChilds = GetChildProductTypeIds(productTypeIds);

            query = query.Where(p => productTypeIdsWithChilds.Contains(p.ProductTypeId));
        }

        return query;
    }

    private IQueryable<Product> ApplyPriceRangeFilter(IQueryable<Product> query, PriceRange? priceRange)
    {
        if (priceRange != null)
        {
            query = query.Where(p => p.ProductVariants.Any(s => s.UnitPrice >= priceRange.MinPrice && s.UnitPrice <= priceRange.MaxPrice));
        }

        return query;
    }

    // Implemented an optimization to reduce database roundtrips by batching queries and using in-memory filtering, improving performance significantly.
    private async Task<Result<IQueryable<Product>>> ApplyCustomFiltersAsync(IQueryable<Product> query, Dictionary<string, List<string>> customFilters, CancellationToken cancellationToken = default)
    {
        if (customFilters.Count <= 0)
        {
            return Result.Success(query);
        }

        // define this attributeFilters for better code readability
        var attributeFilters = customFilters
            .Select(kvp => new
            {
                AttributeName = kvp.Key,
                AttributeValues = kvp.Value
            })
            .ToList();

        var attributeNames = customFilters.Keys.ToList();

        var validProductIds = new HashSet<int>(dbContext.Products.Select(p => p.Id));

        // fetch all attributes and their values in a single query
        var allAttributes = await dbContext.ProductTypeAttributes
            .Include(pa => pa.Values)
            .Where(pa => attributeNames.Contains(pa.Name))
            .ToListAsync(cancellationToken);

        var allValues = allAttributes.SelectMany(pa => pa.Values).ToList();

        // fetch all PAV in a single query
        var allRelevantProductAttributeValues = await dbContext.ProductTypeAttributeProductValues
            .Where(apv => allValues.Select(v => v.Id).Contains(apv.AttributeValueId))
            .ToListAsync(cancellationToken);

        // Now this is where the optimization happens
        // the below code is in-memory filtering instead of querying the database for each attribute filter
        foreach (var attributeFilter in attributeFilters)
        {
            var currFilterAttribute = allAttributes.Find(a => string.Equals(a.Name, attributeFilter.AttributeName, StringComparison.OrdinalIgnoreCase));

            if (currFilterAttribute is null)
            {
                return Result.Failure<IQueryable<Product>>(ProductErrors.InvalidAttribute(attributeFilter.AttributeName));
            }

            var currFilterAttributeValueIds = currFilterAttribute.Values
                .Where(av => attributeFilter.AttributeValues.Contains(av.Value))
                .Select(av => av.Id)
                .ToList();

            var productIds = allRelevantProductAttributeValues
                .Where(pv => currFilterAttributeValueIds.Contains(pv.AttributeValueId))
                .Select(pv => pv.ProductId)
                .ToList();

            validProductIds.IntersectWith(productIds);

            if (validProductIds.Count == 0)
            {
                return Result.Failure<IQueryable<Product>>(ProductErrors.NotFound);
            }
        }

        query = query.Where(p => validProductIds.Contains(p.Id));

        return Result.Success(query);
    }

    private HashSet<int> GetChildProductTypeIds(List<int> parentProductTypeIds)
    {
        var result = new HashSet<int>(parentProductTypeIds);
        foreach (var parentProductTypeId in parentProductTypeIds)
        {
            result.UnionWith(GetChildProductTypeIds(parentProductTypeId));
        }

        return result;
    }

    private HashSet<int> GetChildProductTypeIds(int parentProductTypeId)
    {
        var childProductTypeIds = dbContext.ProductTypes
            .Where(pt => pt.ParentProductTypeId == parentProductTypeId)
            .Select(pt => pt.Id)
            .ToHashSet();

        foreach (var childProductTypeId in new HashSet<int>(childProductTypeIds))
        {
            childProductTypeIds.UnionWith(GetChildProductTypeIds(childProductTypeId));
        }

        return childProductTypeIds;
    }
}
