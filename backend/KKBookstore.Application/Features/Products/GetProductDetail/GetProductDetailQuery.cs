using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetProductDetail;

public record GetProductDetailQuery(int ProductId) : IRequest<Result<GetProductResponse>>;

public class GetProductDetailQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetProductDetailQuery, Result<GetProductResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetProductResponse>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // Query to fetch the product and related data
        var product = await _dbContext.Products
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == request.ProductId)
            .Include(p => p.ProductType)
            .Include(p => p.UnitMeasure)
            .Include(p => p.ProductImages)
            .Include(p => p.Skus)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetProductResponse>(ProductErrors.NotFound);
        }

        var skus = await _dbContext.Skus
            .Where(s => s.ProductId == product.Id && s.IsActive)
            .Include(s => s.SkuOptionValues)
                .ThenInclude(so => so.Option)
            .Include(s => s.SkuOptionValues)
                .ThenInclude(so => so.OptionValue)
            .Include(s => s.SkuValue)
            .Include(s => s.Dimension)
            .ToListAsync(cancellationToken);

        product.Skus = skus;

        // Fetch Book Authors if the product is a book
        if (product.IsBook)
        {
            var bookAuthors = await _dbContext.BookAuthors
                .Include(ab => ab.Author)
                .Where(wb => wb.ProductId == product.Id)
                .ToListAsync(cancellationToken);

            product.BookAuthors = bookAuthors;
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

        // Map to ProductDetailDto
        var productDto = mapper.Map<GetProductResponse>(product);


        productDto.productTypeAttributes = productTypeAttributeValues
            .Select(pav => new GetProductResponse.ProductTypeAttribute()
            {
                ProductTypeId = pav.Product.ProductTypeId,
                AttributeValueId = pav.AttributeValueId,
                AttributeId = pav.AttributeValue.ProductTypeAttribute.Id,
                Name = pav.AttributeValue.ProductTypeAttribute.Name,
                Value = pav.AttributeValue.Value
            });

        // Calculate min and max prices
        if (product.Skus.Count != 0)
        {
            productDto.MinUnitPrice = product.Skus.Min(s => s.UnitPrice);
            productDto.MaxUnitPrice = product.Skus.Max(s => s.UnitPrice);
            productDto.MinRecommendedRetailPrice = product.Skus.Min(s => s.RecommendedRetailPrice);
            productDto.MaxRecommendedRetailPrice = product.Skus.Max(s => s.RecommendedRetailPrice);
        }

        return Result.Success(productDto);
    }
}
