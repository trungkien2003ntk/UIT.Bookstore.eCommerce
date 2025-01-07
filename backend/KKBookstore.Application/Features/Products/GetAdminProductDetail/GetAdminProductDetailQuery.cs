using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetAdminProductDetail;

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
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<AdminProductDto>(ProductErrors.NotFound);
        }
        var productDto = new AdminProductDto
        {
            Id = product.Id,
            Name = product.Name,
            ProductTypeId = product.ProductTypeId,
            Description = product.Description,
            IsBook = product.IsBook,
            IsActive = product.IsActive,
            UnitMeasureId = product.UnitMeasureId,
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
            ProductVariants = product.ProductVariants.Select(pv => new ProductVariantDto
            {
                Id = pv.Id,
                RecommendedRetailPrice = pv.RecommendedRetailPrice,
                UnitPrice = pv.UnitPrice,
                Weight = pv.Weight,
                Dimension = pv.Dimension,
                TaxRate = pv.TaxRate,
                Comment = pv.Comment,
                StockQuantity = pv.StockQuantity,
                VariantOptions = pv.ProductVariantOptionValues.Select(pov => new ProductVariantDto.VariantOptionDto
                {
                    ProductOptionId = pov.OptionId,
                    ProductOptionValueId = pov.OptionValueId,
                    Value = pov.OptionValue.Value,
                    Name = pov.Option.Name,
                }).ToList()
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