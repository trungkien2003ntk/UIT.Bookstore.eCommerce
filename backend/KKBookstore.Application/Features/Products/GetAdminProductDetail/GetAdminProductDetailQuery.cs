using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.GetAdminProductDetail;

public record GetAdminProductDetailQuery(int ProductId) : IRequest<Result<AdminProductDto>>;

public class GetAdminProductDetailQueryHandler : IRequestHandler<GetAdminProductDetailQuery, Result<AdminProductDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAdminProductDetailQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<AdminProductDto>> Handle(GetAdminProductDetailQuery request, CancellationToken cancellationToken)
    {        
        var product = await _dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == request.ProductId)
            .Include(x => x.ProductType)
            .Include(x => x.UnitMeasure)
            .Include(x => x.AttributeProductValues)
                .ThenInclude(x => x.AttributeValue)
                    .ThenInclude(x => x.ProductTypeAttribute)
            .Include(x => x.ProductVariants)
                .ThenInclude(x => x.ProductVariantOptionValues)
                        .ThenInclude(x => x.Option)
            .Include(x => x.ProductVariants)
                .ThenInclude(x => x.ProductVariantOptionValues)
                        .ThenInclude(x => x.OptionValue)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Inventories)
                    .ThenInclude(i => i.Warehouse)
                        .ThenInclude(w => w!.Address)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings)
                    .ThenInclude(r => r.Customer)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings)
                    .ThenInclude(r => r.Likes)
            .Include(p => p.Ratings)
                .ThenInclude(r => r.Customer)
            .Include(p => p.Ratings)
                .ThenInclude(r => r.Likes)
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<AdminProductDto>(ProductErrors.NotFound);
        }
        
        // Calculate overall product rating directly from Product.Ratings
        decimal? overallRating = null;
        int totalRatingsCount = 0;
        
        if (product.Ratings != null && product.Ratings.Any())
        {
            overallRating = Convert.ToDecimal(product.Ratings.Average(r => r.RatingValue));
            totalRatingsCount = product.Ratings.Count;
        }
        
        var productDto = new AdminProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku?.Value,
            ProductTypeId = product.ProductTypeId,
            Description = product.Description,
            IsBook = product.IsBook,
            IsActive = product.IsActive,
            UnitMeasureId = product.UnitMeasureId,
            AverageRating = overallRating,
            RatingsCount = totalRatingsCount,
            ProductType = new ProductTypeDto
            {
                Id = product.ProductType.Id,
                Level = product.ProductType.Level,
                DisplayName = product.ProductType.DisplayName,
                Description = product.ProductType.Description,
                ParentProductTypeId = product.ProductType.ParentProductTypeId,
                ProductTypeCode = product.ProductType.ProductTypeCode
            },
            UnitMeasure = new UnitMeasureDto
            {
                Id = product.UnitMeasure.Id,
                Name = product.UnitMeasure.Name
            },
            AttributeProductValues = product.AttributeProductValues.Select(apv => new ProductTypeAttributeProductValueDto
            {
                AttributeId = apv.AttributeValue.ProductTypeAttributeId,
                AttributeValueId = apv.AttributeValueId,
                Name = apv.AttributeValue.ProductTypeAttribute.Name,
                Value = apv.AttributeValue.Value
            }).ToList(),
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

                return new ProductVariantDto
                {
                    Id = pv.Id,
                    RecommendedRetailPrice = pv.RecommendedRetailPrice,
                    UnitPrice = pv.UnitPrice,
                    Weight = pv.Weight,
                    Dimension = pv.Dimension,
                    TaxRate = pv.TaxRate,
                    Comment = pv.Comment,
                    StockQuantity = pv.StockQuantity,
                    AverageRating = avgRating,
                    RatingsCount = ratingsCount,
                    Ratings = pv.Ratings?.Select(r => new RatingDto
                    {
                        Id = r.Id,
                        Comment = r.Comment ?? string.Empty,
                        RatingValue = r.RatingValue,
                        CustomerId = r.CustomerId,
                        CustomerName = r.Customer?.FirstName ?? "Anonymous",
                        CreationTime = r.CreationTime ?? DateTimeOffset.Now,
                        ProductVariantId = r.ProductVariantId,
                        LikesCount = r.Likes?.Count ?? 0,
                        Response = r.Response ?? string.Empty,
                        IsReported = r.ReportedCount > 0,
                        VariantOptions = pv.ProductVariantOptionValues?.Select(pov => new ProductVariantOptionDto
                        {
                            ProductOptionId = pov.OptionId,
                            ProductOptionValueId = pov.OptionValueId,
                            Name = pov.Option?.Name ?? string.Empty,
                            Value = pov.OptionValue?.Value ?? string.Empty
                        }).ToList() ?? new List<ProductVariantOptionDto>()
                    }).ToList() ?? new List<RatingDto>(),
                    VariantOptions = pv.ProductVariantOptionValues?.Select(pov => new ProductVariantDto.VariantOptionDto
                    {
                        ProductOptionId = pov.OptionId,
                        ProductOptionValueId = pov.OptionValueId,
                        Value = pov.OptionValue?.Value ?? string.Empty,
                        Name = pov.Option?.Name ?? string.Empty,
                    }).ToList() ?? new List<ProductVariantDto.VariantOptionDto>(),
                    StockBreakdowns = pv.Inventories?.Select(inv => new StockBreakdownDto
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
                    }).ToList() ?? new List<StockBreakdownDto>()
                };
            }).ToList(),
            ProductImages = product.ProductImages.Select(pi => new ProductImageDto
            {
                Id = pi.Id,
                ThumbnailImageUrl = pi.ThumbnailImageUrl,
                LargeImageUrl = pi.LargeImageUrl
            }).ToList()
        };

        return Result.Success(productDto);
    }
}