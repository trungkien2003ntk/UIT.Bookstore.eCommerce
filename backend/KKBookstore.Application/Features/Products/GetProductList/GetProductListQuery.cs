using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Products.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.GetProductList;

public record GetProductListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<ProductSummary>>>
{
    // other properties for filtering 
    public List<int>? ProductTypeIds { get; set; }
    public List<int>? ExcludeProductIds { get; set; }
    public PriceRange? PriceRange { get; set; }
    public Dictionary<string, List<string>> CustomFilters { get; set; } = [];
    public bool IsActive { get; set; } = true;
    public string? SearchQuery { get; set; }
}

// todo: this class is doing too much, consider refactoring
public class GetProductListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetProductListQuery, Result<PagedResult<ProductSummary>>>
{
    public async Task<Result<PagedResult<ProductSummary>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Phase 1: Apply all filters but minimize includes for the filtering phase
            IQueryable<Product> baseQuery = dbContext.Products
                .ApplyFullTextSearch(
                    request.SearchQuery,
                    FullTextSearchMode.All,
                    fullTextFields: [p => p.Name/*, p => p.Description*/],
                    likeFields: [p => p.Id.ToString()])
                .AsNoTracking();

            baseQuery = await ApplyProductIdsFilter(baseQuery, request.ProductTypeIds);
            baseQuery = ApplyPriceRangeFilter(baseQuery, request.PriceRange);
            baseQuery = ApplyExcludeProducts(baseQuery, request.ExcludeProductIds);
            baseQuery = baseQuery
                .Where(p => p.IsActive == request.IsActive);

            var customFilterResult = await ApplyCustomFiltersAsync(baseQuery, request.CustomFilters, cancellationToken);
            if (customFilterResult.IsFailure)
            {
                return Result.Failure<PagedResult<ProductSummary>>(customFilterResult.Error);
            }

            baseQuery = customFilterResult.Value;

            // Get total count before pagination for accurate paging
            var totalCount = await baseQuery.CountAsync(cancellationToken);
            if (totalCount == 0)
            {
                return Result.Failure<PagedResult<ProductSummary>>(ProductErrors.NotFound);
            }

            // Apply sorting
            IQueryable<Product> sortedQuery = ApplySorting(baseQuery, request.SortBy, request.SortDirection);

            // Phase 2: Get just the IDs for the current page
            var pagedProductIds = await sortedQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            // Phase 3: Load only the detailed data needed for the current page
            // with optimized includes
            var pagedProductsWithDetails = await dbContext.Products
                .AsNoTracking()
                .Where(p => pagedProductIds.Contains(p.Id))
                .Include(p => p.ProductType)
                .Include(p => p.ProductImages.Take(1)) // Only first image
                .Include(p => p.Ratings)
                .Include(p => p.ProductVariants.OrderBy(pv => pv.UnitPrice)) // Limit variants
                    .ThenInclude(pv => pv.ProductVariantOptionValues!) // Limit option values
                        .ThenInclude(pov => pov.Option)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantOptionValues!)
                        .ThenInclude(pov => pov.OptionValue)
                .ToListAsync(cancellationToken);
            pagedProductsWithDetails = pagedProductsWithDetails
                .OrderBy(p => pagedProductIds.IndexOf(p.Id))
                .ToList();
            // Load inventory data separately to reduce join complexity
            var productIds = pagedProductsWithDetails.Select(p => p.Id).ToList();
            var variantIds = pagedProductsWithDetails
                .SelectMany(p => p.ProductVariants.Select(v => v.Id))
                .ToList();

            var inventories = await dbContext.Inventories
                .AsNoTracking()
                .Where(i => i.IsActive && variantIds.Contains(i.ProductVariantId))
                .Include(i => i.Warehouse)
                .ToListAsync(cancellationToken);

            // Create lookup for efficient assignment
            var inventoryByVariantId = inventories
                .GroupBy(i => i.ProductVariantId)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Assign inventories to variants
            foreach (var product in pagedProductsWithDetails)
            {
                foreach (var variant in product.ProductVariants)
                {
                    if (inventoryByVariantId.TryGetValue(variant.Id, out var variantInventories))
                    {
                        variant.Inventories = variantInventories;
                    }
                }
            }

            // Load sold counts in a single efficient query
            var soldCounts = await GetSoldCountsAsync(productIds, cancellationToken);

            // Create page result
            var products = new PagedResult<Product>(
                pagedProductsWithDetails,
                totalCount,
                request.PageSize,
                request.PageNumber
            );

            // Map to DTOs
            var result = MapToProductSummaryResult(products, soldCounts);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            // Log exception here
            return Result.Failure<PagedResult<ProductSummary>>(Error.Failure("Error.Internal", ex.Message));
        }
    }

    private IQueryable<Product> ApplySorting(IQueryable<Product> query, string sortBy, string sortDirection)
    {
        var dtoValidSortProperties = new List<string>
        {
            nameof(ProductSummary.MinUnitPrice),
            nameof(ProductSummary.MinRecommendedRetailPrice)
        };

        var internalValidSortProperties = new List<string>
        {
            nameof(Product.CreationTime),
            nameof(Product.Name),
            nameof(Product.Id)
        };

        List<string> sortValidProperties = [.. internalValidSortProperties, .. dtoValidSortProperties];
        var sortValidateResult = ValidateSortProperty(sortBy, sortValidProperties);

        if (sortValidateResult.IsFailure)
        {
            // Default to sorting by ID if invalid
            sortBy = nameof(Product.Id);
            sortDirection = "asc";
        }

        bool isAscending = sortDirection.ToLower() == "asc";

        // Apply appropriate sorting based on property
        return sortBy switch
        {
            nameof(ProductSummary.MinUnitPrice) => isAscending
                ? query.OrderBy(p => p.ProductVariants.Min(pv => pv.UnitPrice))
                : query.OrderByDescending(p => p.ProductVariants.Min(pv => pv.UnitPrice)),

            nameof(ProductSummary.MinRecommendedRetailPrice) => isAscending
                ? query.OrderBy(p => p.ProductVariants.Min(pv => pv.RecommendedRetailPrice))
                : query.OrderByDescending(p => p.ProductVariants.Min(pv => pv.RecommendedRetailPrice)),

            nameof(Product.CreationTime) => isAscending
                ? query.OrderBy(p => p.CreationTime)
                : query.OrderByDescending(p => p.CreationTime),

            nameof(Product.Name) => isAscending
                ? query.OrderBy(p => p.Name)
                : query.OrderByDescending(p => p.Name),

            // Default to ID
            _ => isAscending
                ? query.OrderBy(p => p.Id)
                : query.OrderByDescending(p => p.Id)
        };
    }

    private Result ValidateSortProperty(string sortProperty, List<string> validSortProperties)
    {
        if (string.IsNullOrWhiteSpace(sortProperty))
        {
            return Result.Success();
        }

        if (!validSortProperties.Contains(sortProperty, StringComparer.OrdinalIgnoreCase))
        {
            return Result.Failure<ProductSummary>(ProductErrors.InvalidAttributeValue(nameof(GetProductListQuery.SortBy), validSortProperties));
        }

        return Result.Success();
    }

    private PagedResult<ProductSummary> MapToProductSummaryResult(PagedResult<Product> paginatedProducts, Dictionary<int, int> soldCounts)
    {
        return new PagedResult<ProductSummary>(
            paginatedProducts.Items.Select(p => new ProductSummary()
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku?.Value ?? string.Empty,
                Description = p.Description,
                ProductTypeId = p.ProductTypeId,
                ProductTypeName = p.ProductType.DisplayName,
                ThumbnailImageUrl = p.GetFirstThumbnailImageUrl(),
                IsBook = p.IsBook,
                SoldCount = soldCounts.TryGetValue(p.Id, out var count) ? count : 0,
                MinUnitPrice = p.ProductVariants.Count != 0 ? p.ProductVariants.Min(s => s.UnitPrice) : 0,
                MinRecommendedRetailPrice = p.ProductVariants.Count != 0 ? p.ProductVariants.Min(s => s.RecommendedRetailPrice) : 0,
                AverageRating = (decimal)(p.Ratings.Count > 0 ? p.Ratings.Average(r => r.RatingValue) : 0),
                CreationTime = p.CreationTime,
                IsActive = p.IsActive,
                TotalStockQuantity = p.ProductVariants.Sum(pv => pv.StockQuantity),
                Variants = p.ProductVariants.Select(pv => new ProductVariantSummaryDto
                {
                    Id = pv.Id,
                    Sku = pv.SkuValue.Value,
                    UnitPrice = pv.UnitPrice,
                    RecommendedRetailPrice = pv.RecommendedRetailPrice,
                    LastestUnitCost = pv.LastestUnitCost,
                    StockQuantity = pv.StockQuantity,
                    ThumbnailImageUrl = pv.GetThumbnailImageUrl() ?? string.Empty,
                    OptionValues = pv.ProductVariantOptionValues?.Select(pov => new OptionValueDto
                    {
                        Name = pov.Option.Name,
                        Value = pov.OptionValue.Value
                    }),
                    StockBreakdowns = pv.Inventories is null ? [] : pv.Inventories
                        .Where(i => i.IsActive)
                        .GroupBy(i => new { i.WarehouseId, i.Warehouse!.Name })
                        .Select(g => new StockSummaryDto()
                        {
                            BranchId = g.Key.WarehouseId!.Value,
                            BranchName = g.Key.Name,
                            StockQuantity = g.Sum(x => x.StockQuantity),
                            IsActive = true
                        })
                }).ToList()
            }).ToList(),
            paginatedProducts.TotalCount,
            paginatedProducts.PageSize,
            paginatedProducts.PageNumber
        );
    }

    private IQueryable<Product> ApplyExcludeProducts(IQueryable<Product> query, List<int>? excludeProductIds)
    {
        if (excludeProductIds is not null && excludeProductIds.Count > 0)
        {
            query = query.Where(p => !excludeProductIds.Contains(p.Id));
        }

        return query;
    }

    private async Task<IQueryable<Product>> ApplyProductIdsFilter(IQueryable<Product> query, List<int>? productTypeIds)
    {
        if (productTypeIds?.Count > 0)
        {
            var productTypeIdsWithChilds = await GetAllChildProductTypeIdsAsync(productTypeIds);

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

    public async Task<Result<IQueryable<Product>>> ApplyCustomFiltersAsync(
        IQueryable<Product> query,
        Dictionary<string, List<string>> customFilters,
        CancellationToken cancellationToken = default)
    {
        if (customFilters == null || customFilters.Count == 0)
        {
            return Result.Success(query);
        }

        try
        {
            // Apply each filter directly through SQL expressions instead of loading into memory
            foreach (var filter in customFilters)
            {
                string attributeName = filter.Key;
                List<string> attributeValues = filter.Value;

                // Skip empty filters
                if (attributeValues.Count == 0)
                {
                    continue;
                }

                // Apply filter using SQL subquery instead of in-memory filtering
                query = query.Where(p =>
                    dbContext.ProductTypeAttributeProductValues
                        .Any(pav =>
                            pav.ProductId == p.Id &&
                            dbContext.ProductTypeAttributeValues
                                .Any(av =>
                                    av.Id == pav.AttributeValueId &&
                                    attributeValues.Contains(av.Value) &&
                                    dbContext.ProductTypeAttributes
                                        .Any(pa =>
                                            pa.Id == av.ProductTypeAttributeId &&
                                            pa.Name == attributeName)
                                )
                        )
                );
            }

            return Result.Success(query);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure<IQueryable<Product>>(ProductErrors.InvalidAttribute(ex.Message));
        }
    }

    private async Task<Dictionary<int, int>> GetSoldCountsAsync(List<int> productIds, CancellationToken cancellationToken)
    {
        return await dbContext.OrderLines
            .Where(ol =>
                productIds.Contains(ol.ProductVariant.ProductId) &&
                (ol.Order.Status == OrderStatus.Received || ol.Order.Status == OrderStatus.Delivered))
            .GroupBy(ol => ol.ProductVariant.ProductId)
            .Select(g => new { ProductId = g.Key, SoldCount = g.Count() })
            .ToDictionaryAsync(x => x.ProductId, x => x.SoldCount, cancellationToken);
    }

    // Get all product type IDs including children, similar to GetProductTypeListQuery approach
    private async Task<HashSet<int>> GetAllChildProductTypeIdsAsync(List<int> parentProductTypeIds)
    {
        // Get all product types to build hierarchy in memory (similar to GetProductTypeListQuery)
        var allProductTypes = await dbContext.ProductTypes
            .Select(pt => new ProductTypeDto { Id = pt.Id, ParentProductTypeId = pt.ParentProductTypeId })
            .ToListAsync();

        // Build a lookup table to find children for each product type
        var lookup = allProductTypes.ToLookup(p => p.ParentProductTypeId);

        var allIds = new HashSet<int>(parentProductTypeIds);

        // For each parent ID, recursively get all child IDs
        foreach (var parentId in parentProductTypeIds)
        {
            GetAllChildIds(parentId, lookup, allIds);
        }

        return allIds;
    }

    private void GetAllChildIds(int parentId, ILookup<int?, ProductTypeDto> lookup, HashSet<int> allIds)
    {
        // Get direct children of the current parent
        var children = lookup[parentId];

        foreach (var child in children)
        {
            if (allIds.Add(child.Id)) // Add returns true if it was actually added (not already present)
            {
                // Recursively get children of this child
                GetAllChildIds(child.Id, lookup, allIds);
            }
        }
    }

    private class ProductTypeDto
    {
        public int Id { get; set; }
        public int? ParentProductTypeId { get; set; }
    }
}