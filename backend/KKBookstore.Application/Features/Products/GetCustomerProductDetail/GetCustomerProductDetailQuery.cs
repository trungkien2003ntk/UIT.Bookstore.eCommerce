using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Features.Products.GetCustomerProductDetail.GetCustomerProductDetailResponse;

namespace KKBookstore.Features.Products.GetCustomerProductDetail;

public record GetCustomerProductDetailQuery(int ProductId) : IRequest<Result<GetCustomerProductDetailResponse>>;

public class GetCustomerProductDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerProductDetailQuery, Result<GetCustomerProductDetailResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetCustomerProductDetailResponse>> Handle(GetCustomerProductDetailQuery request, CancellationToken cancellationToken)
    {        // Query to fetch the product and related data        
        var product = await _dbContext.Products
            .AsNoTracking()
            .AsSplitQuery()
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == request.ProductId)
            .Include(p => p.ProductType)
            .Include(p => p.UnitMeasure)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.ProductVariantOptionValues)!
                    .ThenInclude(pov => pov.Option)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.ProductVariantOptionValues)!
                    .ThenInclude(pov => pov.OptionValue)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Inventories)!
                    .ThenInclude(i => i.Warehouse)
                        .ThenInclude(w => w!.Address)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings.Where(r => r.Status != RatingStatus.Hidden))!
                    .ThenInclude(r => r.Customer)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings.Where(r => r.Status != RatingStatus.Hidden))!
                    .ThenInclude(r => r.Likes)
            .Include(p => p.Ratings.Where(r => r.Status != RatingStatus.Hidden))
                .ThenInclude(r => r.Customer)
            .Include(p => p.Ratings.Where(r => r.Status != RatingStatus.Hidden))
                .ThenInclude(r => r.Likes)
            .Include(p => p.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetCustomerProductDetailResponse>(ProductErrors.NotFound);
        }

        var productResponse = await MapToResponseAsync(product, cancellationToken);

        return Result.Success(productResponse);
    }

    public async Task<GetCustomerProductDetailResponse> MapToResponseAsync(Product product, CancellationToken cancellationToken)
    {
        var productTypeAttributeValues = await _dbContext.ProductTypeAttributeProductValues
            .Where(apv => apv.ProductId == product.Id)
            .Include(apv => apv.AttributeValue)
                .ThenInclude(av => av.ProductTypeAttribute)
            .Include(apv => apv.Product)
            .Select(apv => new ProductTypeAttributeProductValue()
            {
                Product = new()
                {
                    ProductTypeId = apv.Product.ProductTypeId
                },
                AttributeValue = new()
                {
                    ProductTypeAttribute = new()
                    {
                        Id = apv.AttributeValue.ProductTypeAttribute.Id,
                        Name = apv.AttributeValue.ProductTypeAttribute.Name
                    },
                    Value = apv.AttributeValue.Value
                },
                AttributeValueId = apv.AttributeValueId
            })
            .ToListAsync(cancellationToken);

        // Calculate overall product rating directly from Product.Ratings
        decimal? overallRating = null;
        int totalRatingsCount = 0;

        if (product.Ratings != null && product.Ratings.Any())
        {
            overallRating = Convert.ToDecimal(product.Ratings.Average(r => r.RatingValue));
            totalRatingsCount = product.Ratings.Count;
        }

        var productResponse = new GetCustomerProductDetailResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku?.Value,
            UnitMeasureName = product.UnitMeasure?.Name,
            Description = product.Description,
            ProductTypeId = product.ProductTypeId,
            ProductTypeName = product.ProductType?.DisplayName ?? string.Empty,
            IsBook = product.IsBook,
            AverageRating = overallRating,
            RatingsCount = totalRatingsCount,
            ThumbnailImageUrls = product.ProductImages?.Select(pi => pi.ThumbnailImageUrl ?? string.Empty) ?? Array.Empty<string>(),
            LargeImageUrls = product.ProductImages?.Select(pi => pi.LargeImageUrl ?? string.Empty) ?? Array.Empty<string>(),
            TotalStockQuantity = product.ProductVariants.Sum(pv => pv.StockQuantity),
            Authors = product.IsBook && product.BookAuthors != null ? product.BookAuthors.Select(ba => new AuthorDto()
            {
                Id = ba.Author.Id,
                Name = ba.Author.Name
            }) : null,

            ProductVariants = product.ProductVariants.Select(pv =>
            {
                // Calculate average rating for this variant
                decimal? avgRating = null;
                int ratingsCount = 0;

                if (pv.Ratings != null && pv.Ratings.Any())
                {
                    avgRating = Convert.ToDecimal(pv.Ratings.Average(r => r.RatingValue));
                    ratingsCount = pv.Ratings.Count;
                }

                return new CustomerProductVariantDto()
                {
                    Id = pv.Id,
                    Sku = pv.SkuValue?.Value,
                    UnitPrice = pv.UnitPrice,
                    RecommendedRetailPrice = pv.RecommendedRetailPrice,
                    Height = pv.Dimension?.Height ?? 0,
                    Width = pv.Dimension?.Width ?? 0,
                    Length = pv.Dimension?.Length ?? 0,
                    Weight = pv.Weight,
                    Status = pv.StockQuantity > 0 ? "InStock" : "OutOfStock",
                    StockQuantity = pv.StockQuantity,
                    AverageRating = avgRating,
                    RatingsCount = ratingsCount,
                    OptionValues = pv.ProductVariantOptionValues?.Select(pov => new OptionValueDto()
                    {
                        Name = pov.Option?.Name ?? string.Empty,
                        Value = pov.OptionValue?.Value ?? string.Empty
                    }) ?? Array.Empty<OptionValueDto>(),
                    Ratings = pv.Ratings?
                        .Select(r => new RatingDto
                        {
                            Id = r.Id,
                            Comment = r.Comment,
                            RatingValue = r.RatingValue,
                            CustomerId = r.CustomerId,
                            CustomerName = r.Customer?.UserName ?? "Anonymous",
                            CreationTime = r.CreationTime!.Value,
                            ProductVariantId = r.ProductVariantId,
                            LikesCount = r.Likes?.Count ?? 0,
                            Response = r.Response,
                            IsReported = r.ReportsCount > 0,
                            VariantOptions = pv.ProductVariantOptionValues?.Select(pov => new ProductVariantOptionDto
                            {
                                ProductOptionId = pov.OptionId,
                                ProductOptionValueId = pov.OptionValueId,
                                Name = pov.Option?.Name ?? string.Empty,
                                Value = pov.OptionValue?.Value ?? string.Empty
                            }).ToList() ?? new List<ProductVariantOptionDto>()
                        }).ToList() ?? new List<RatingDto>(),
                    StockBreakdowns = pv.Inventories.Select(inv => new StockBreakdownDto
                    {
                        Id = inv.Id,
                        BranchId = inv.WarehouseId ?? 0,
                        BranchName = inv.Warehouse?.Name ?? "Unknown",
                        Description = inv.Warehouse?.Description ?? string.Empty,
                        StockQuantity = inv.StockQuantity,
                        IsActive = inv.IsActive,
                        Address = inv.Warehouse?.Address != null ? new BranchAddressDto
                        {
                            PhoneNumber = inv.Warehouse.Address.PhoneNumber,
                            ProvinceId = inv.Warehouse.Address.ProvinceId,
                            ProvinceName = inv.Warehouse.Address.ProvinceName,
                            DistrictId = inv.Warehouse.Address.DistrictId,
                            DistrictName = inv.Warehouse.Address.DistrictName,
                            CommuneCode = inv.Warehouse.Address.CommuneCode,
                            CommuneName = inv.Warehouse.Address.CommuneName,
                            DetailAddress = inv.Warehouse.Address.DetailAddress,
                            Type = inv.Warehouse.Address.Type
                        } : null
                    }).ToList()
                };
            }),
            ProductVariantOptions = product.ProductVariants.SelectMany(pv => pv.ProductVariantOptionValues)
                .GroupBy(pov => pov.OptionId)
                .Select(g => new ProductVariantOption()
                {
                    Id = g.Key,
                    Name = g.First().Option.Name,
                    Values = g.Select(pov => pov.OptionValue.Value).Distinct(),
                    ThumbnailImageUrls = g.Select(pov => pov.OptionValue.ThumbnailImageUrl).Distinct(),
                    LargeImageUrls = g.Select(pov => pov.OptionValue.LargeImageUrl).Distinct()
                }),
            ProductTypeAttributes = productTypeAttributeValues
                .Select(pav => new GetCustomerProductDetailResponse.ProductTypeAttribute()
                {
                    ProductTypeId = pav.Product.ProductTypeId,
                    AttributeValueId = pav.AttributeValueId,
                    AttributeId = pav.AttributeValue.ProductTypeAttribute.Id,
                    Name = pav.AttributeValue.ProductTypeAttribute.Name,
                    Value = pav.AttributeValue.Value
                }),
            ProductTypes = await _dbContext.ProductTypes
                .Where(pt => pt.Id == product.ProductTypeId)
                .OrderBy(pt => pt.Level)
                .Select(pt => new ProductTypeDto()
                {
                    Id = pt.Id,
                    DisplayName = pt.DisplayName,
                    Level = pt.Level
                })
                .ToListAsync(cancellationToken)
        };

        // Calculate min and max prices
        if (product.ProductVariants.Count != 0)
        {
            productResponse.MinUnitPrice = productResponse.ProductVariants.Min(s => s.UnitPrice);
            productResponse.MaxUnitPrice = productResponse.ProductVariants.Max(s => s.UnitPrice);
            productResponse.MinRecommendedRetailPrice = productResponse.ProductVariants.Min(s => s.RecommendedRetailPrice);
            productResponse.MaxRecommendedRetailPrice = productResponse.ProductVariants.Max(s => s.RecommendedRetailPrice);
        }

        return productResponse;
    }
}
