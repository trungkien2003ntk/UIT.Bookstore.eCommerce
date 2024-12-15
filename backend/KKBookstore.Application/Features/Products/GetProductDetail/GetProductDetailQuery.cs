using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.Products.GetProductDetail.GetProductDetailResponse;

namespace KKBookstore.Application.Features.Products.GetProductDetail;

public record GetProductDetailQuery(int ProductId) : IRequest<Result<GetProductDetailResponse>>;

public class GetProductDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetProductDetailQuery, Result<GetProductDetailResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetProductDetailResponse>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // Query to fetch the product and related data
        var product = await _dbContext.Products
            .AsNoTracking()
            .AsSplitQuery()
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == request.ProductId)
            .Include(p => p.ProductType)
            .Include(p => p.UnitMeasure)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.ProductVariantOptionValues)
                    .ThenInclude(pov => pov.Option)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.ProductVariantOptionValues)
                    .ThenInclude(pov => pov.OptionValue)
            .Include(p => p.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetProductDetailResponse>(ProductErrors.NotFound);
        }

        var productResponse = await MapToResponseAsync(product, cancellationToken);

        return Result.Success(productResponse);
    }

    public async Task<GetProductDetailResponse> MapToResponseAsync(Product product, CancellationToken cancellationToken)
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

        var productResponse = new GetProductDetailResponse
        {
            Id = product.Id,
            Name = product.Name,
            UnitMeasureName = product.UnitMeasure.Name,
            Description = product.Description,
            ProductTypeId = product.ProductTypeId,
            ProductTypeName = product.ProductType.DisplayName,
            IsBook = product.IsBook,
            ThumbnailImageUrls = product.ProductImages.Select(pi => pi.ThumbnailImageUrl),
            LargeImageUrls = product.ProductImages.Select(pi => pi.LargeImageUrl),
            Authors = product.IsBook ? product.BookAuthors.Select(ba => new AuthorDto()
            {
                Id = ba.Author.Id,
                Name = ba.Author.Name
            }) : null,
            ProductVariants = product.ProductVariants.Select(pv => new ProductVariantDto()
            {
                Id = pv.Id,
                SkuValue = pv.SkuValue.Value,
                UnitPrice = pv.UnitPrice,
                RecommendedRetailPrice = pv.RecommendedRetailPrice,
                Height = pv.Dimension.Height,
                Width = pv.Dimension.Width,
                Length = pv.Dimension.Length,
                OptionValues = pv.ProductVariantOptionValues.Select(pov => new OptionValueDto()
                {
                    Name = pov.Option.Name,
                    Value = pov.OptionValue.Value
                })
            }),
            ProductVariantOptions = product.ProductVariants.SelectMany(pv => pv.ProductVariantOptionValues)
                .GroupBy(pov => pov.OptionId)
                .Select(g => new ProductVariantOption()
                {
                    Id = g.Key,
                    Name = g.First().Option.Name,
                    Values = g.Select(pov => pov.OptionValue.Value),
                    ThumbnailImageUrls = g.Select(pov => pov.OptionValue.ThumbnailImageUrl),
                    LargeImageUrls = g.Select(pov => pov.OptionValue.LargeImageUrl)
                }),
            ProductTypeAttributes = productTypeAttributeValues
                .Select(pav => new GetProductDetailResponse.ProductTypeAttribute()
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
                    Name = pt.DisplayName,
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
