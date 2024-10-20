using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.Products.GetProductDetail.GetProductDetailResponse;

namespace KKBookstore.Application.Features.Products.GetProductDetail;

public record GetProductDetailQuery(int ProductId) : IRequest<Result<GetProductDetailResponse>>;

public class GetProductDetailQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetProductDetailQuery, Result<GetProductDetailResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetProductDetailResponse>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // Query to fetch the product and related data
        var product = await _dbContext.Products
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == request.ProductId)
            .Include(p => p.ProductType)
            .Include(p => p.UnitMeasure)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetProductDetailResponse>(ProductErrors.NotFound);
        }

        // fetch the product variants
        product.ProductVariants = await GetProductVariants(product, cancellationToken);

        // fetch the book authors if the product is a book
        if (product.IsBook)
        {
            product.BookAuthors = await GetBookAuthors(product, cancellationToken);
        }

        // fetch ProductTypeAttributes
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

        // hand map:
        var productDtoHandMap = new GetProductDetailResponse()
        {
            Id = product.Id,
            Name = product.Name,
            //MinUnitPrice = productDto.MinUnitPrice,
            //MaxUnitPrice = productDto.MaxUnitPrice,
            //MinRecommendedRetailPrice = productDto.MinRecommendedRetailPrice,
            //MaxRecommendedRetailPrice = productDto.MaxRecommendedRetailPrice,
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
                })
        };


        productDtoHandMap.productTypeAttributes = productTypeAttributeValues
            .Select(pav => new GetProductDetailResponse.ProductTypeAttribute()
            {
                ProductTypeId = pav.Product.ProductTypeId,
                AttributeValueId = pav.AttributeValueId,
                AttributeId = pav.AttributeValue.ProductTypeAttribute.Id,
                Name = pav.AttributeValue.ProductTypeAttribute.Name,
                Value = pav.AttributeValue.Value
            });

        // Calculate min and max prices
        if (product.ProductVariants.Count != 0)
        {
            productDtoHandMap.MinUnitPrice = product.ProductVariants.Min(s => s.UnitPrice);
            productDtoHandMap.MaxUnitPrice = product.ProductVariants.Max(s => s.UnitPrice);
            productDtoHandMap.MinRecommendedRetailPrice = product.ProductVariants.Min(s => s.RecommendedRetailPrice);
            productDtoHandMap.MaxRecommendedRetailPrice = product.ProductVariants.Max(s => s.RecommendedRetailPrice);
        }

        return Result.Success(productDtoHandMap);
    }

    private async Task<List<ProductVariant>> GetProductVariants(Product? product, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductVariants
                    .Where(s => s.ProductId == product.Id && s.IsActive)
                    .Include(s => s.ProductVariantOptionValues)
                        .ThenInclude(so => so.Option)
                    .Include(s => s.ProductVariantOptionValues)
                        .ThenInclude(so => so.OptionValue)
                    .Include(s => s.SkuValue)
                    .Include(s => s.Dimension)
                    .ToListAsync(cancellationToken);
    }

    private async Task<List<BookAuthor>> GetBookAuthors(Product product, CancellationToken cancellationToken)
    {
        return await _dbContext.BookAuthors
                        .Include(ab => ab.Author)
                        .Where(wb => wb.ProductId == product.Id)
                        .ToListAsync(cancellationToken);
    }
}
