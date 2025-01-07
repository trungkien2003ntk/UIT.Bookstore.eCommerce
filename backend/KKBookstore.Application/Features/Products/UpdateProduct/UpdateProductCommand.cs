using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand : AdminProductDto, IRequest<Result<AdminProductDto>>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<AdminProductDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IApplicationDbContext dbContext, ILogger<UpdateProductCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<AdminProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
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
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            if (product is null)
            {
                return Result.Failure<AdminProductDto>(ProductErrors.NotFound);
            }
            var productType = await _dbContext.ProductTypes.FirstOrDefaultAsync(x => x.Id == request.ProductTypeId, cancellationToken);
            if (productType is null)
            {
                return Result.Failure<AdminProductDto>(ProductTypeErrors.NotFound);
            }

            var unitMeasure = await _dbContext.UnitMeasures.FirstOrDefaultAsync(x => x.Id == request.UnitMeasureId, cancellationToken);
            if (unitMeasure is null)
            {
                return Result.Failure<AdminProductDto>(UnitMeasureErrors.NotFound);
            }


            _logger.LogInformation("Updating product with ID {ProductId}", request.Id);
            product.Name = request.Name;
            product.ProductTypeId = request.ProductTypeId;
            product.Description = request.Description ?? "";
            product.IsBook = request.IsBook;
            product.IsActive = request.IsActive;
            product.UnitMeasureId = request.UnitMeasureId;

            _logger.LogInformation("Updating Product Type");
            request.ProductType ??= new ProductTypeDto()
            {
                Description = productType.Description,
                DisplayName = productType.DisplayName,
                ProductTypeCode = productType.ProductTypeCode,
                ParentProductTypeId = productType.ParentProductTypeId,
                Level = productType.Level,
                Id = productType.Id
            };
            UpdateProductType(product, request.ProductType);

            _logger.LogInformation("Updating Unit Measure");
            request.UnitMeasure ??= new UnitMeasureDto()
            {
                Description = unitMeasure.Description,
                Name = unitMeasure.Name,
                Id = unitMeasure.Id
            };
            UpdateUnitMeasure(product, request.UnitMeasure);

            _logger.LogInformation("Updating Attribute Product Values");
            UpdateAttributeProductValues(product, request.AttributeProductValues);

            _logger.LogInformation("Updating Product Variants");
            UpdateProductVariants(product, request.Id, request.ProductVariants);

            _logger.LogInformation("Updating Product Images");
            UpdateProductImages(product, request.ProductImages);


            _logger.LogInformation("Saving changes to database");
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Product with ID {ProductId} updated successfully", request.Id);
            var result = MapToAdminProductDto(product);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update product with ID {ProductId}", request.Id);
            _logger.LogError("Error: {Message}", ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private void UpdateProductType(Product product, ProductTypeDto productType)
    {
        if (product.ProductType.Id != productType.Id)
        {
            product.ProductType = new ProductType
            {
                Id = productType.Id,
                Level = productType.Level,
                DisplayName = productType.DisplayName,
                Description = productType.Description,
                ParentProductTypeId = productType.ParentProductTypeId
            };
        }
    }

    private void UpdateUnitMeasure(Product product, UnitMeasureDto unitMeasure)
    {
        if (product.UnitMeasure.Id != unitMeasure.Id)
        {
            product.UnitMeasure = new UnitMeasure
            {
                Id = unitMeasure.Id,
                Name = unitMeasure.Name
            };
        }
    }


    private void UpdateAttributeProductValues(Product product, ICollection<ProductTypeAttributeProductValueDto> attributeProductValues)
    {
        var existingAttributeProductValues = product.AttributeProductValues.ToList();

        foreach (var attributeProductValue in attributeProductValues)
        {
            var existingAttributeProductValue = existingAttributeProductValues.FirstOrDefault(x => x.AttributeValueId == attributeProductValue.AttributeValueId);

            if (existingAttributeProductValue is null)
            {
                product.AttributeProductValues.Add(new ProductTypeAttributeProductValue
                {
                    AttributeValueId = attributeProductValue.AttributeValueId
                });
            }
        }

        foreach (var existingAttributeProductValue in existingAttributeProductValues)
        {
            var attributeProductValue = attributeProductValues.FirstOrDefault(x => x.AttributeValueId == existingAttributeProductValue.AttributeValueId);

            if (attributeProductValue is null)
            {
                product.AttributeProductValues.Remove(existingAttributeProductValue);
            }
        }
    }

    private void UpdateProductVariants(Product product, int productDtoId, ICollection<ProductVariantDto> productVariants)
    {
        var existingProductVariants = product.ProductVariants.ToList();
        foreach (var productVariant in productVariants)
        {
            var existingProductVariant = existingProductVariants.FirstOrDefault(x => x.Id == productVariant.Id);
            if (existingProductVariant is null)
            {
                product.ProductVariants.Add(new ProductVariant
                {
                    Id = productVariant.Id,
                    ProductId = productDtoId,
                    RecommendedRetailPrice = productVariant.RecommendedRetailPrice,
                    UnitPrice = productVariant.UnitPrice,
                    TaxRate = productVariant.TaxRate,
                    Comment = productVariant.Comment,
                    ValidFrom = DateTimeOffset.UtcNow,
                    Weight = productVariant.Weight,
                    Dimension = productVariant.Dimension,
                    IsActive = true,
                    Tags = ""
                });
            }
            else
            {
                existingProductVariant.RecommendedRetailPrice = productVariant.RecommendedRetailPrice;
                existingProductVariant.UnitPrice = productVariant.UnitPrice;
                existingProductVariant.TaxRate = productVariant.TaxRate;
                existingProductVariant.Comment = productVariant.Comment;
                existingProductVariant.Weight = productVariant.Weight;
                existingProductVariant.Dimension = productVariant.Dimension;
            }
        }

        foreach (var existingProductVariant in existingProductVariants)
        {
            var productVariant = productVariants.FirstOrDefault(x => x.Id == existingProductVariant.Id);
            if (productVariant is null)
            {
                product.ProductVariants.Remove(existingProductVariant);
            }
        }
    }

    private void UpdateProductImages(Product product, ICollection<ProductImageDto> productImages)
    {
        var existingProductImages = product.ProductImages.ToList();

        foreach (var productImage in productImages)
        {
            var existingProductImage = existingProductImages.FirstOrDefault(x => x.Id == productImage.Id);

            if (existingProductImage is null)
            {
                product.ProductImages.Add(new ProductImage
                {
                    Id = productImage.Id,
                    ProductId = product.Id,
                    ThumbnailImageUrl = productImage.ThumbnailImageUrl,
                    LargeImageUrl = productImage.LargeImageUrl
                });
            }
        }

        foreach (var existingProductImage in existingProductImages)
        {
            var productImage = productImages.FirstOrDefault(x => x.Id == existingProductImage.Id);

            if (productImage is null)
            {
                _logger.LogInformation("Removing product image with ID {ImageId}", existingProductImage.Id);
                product.ProductImages.Remove(existingProductImage);
            }
        }
    }

    private AdminProductDto MapToAdminProductDto(Product product)
    {
        return new AdminProductDto
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
                ParentProductTypeId = product.ProductType.ParentProductTypeId
            },
            UnitMeasure = new UnitMeasureDto
            {
                Id = product.UnitMeasure.Id,
                Name = product.UnitMeasure.Name
            },
            AttributeProductValues = product.AttributeProductValues.Select(x => new ProductTypeAttributeProductValueDto
            {
                AttributeId = x.AttributeValue.ProductTypeAttributeId,
                AttributeValueId = x.AttributeValueId,
                Name = x.AttributeValue.ProductTypeAttribute.Name,
                Value = x.AttributeValue.Value
            }).ToList(),
            ProductVariants = product.ProductVariants.Select(x => new ProductVariantDto
            {
                Id = x.Id,
                RecommendedRetailPrice = x.RecommendedRetailPrice,
                UnitPrice = x.UnitPrice,
                TaxRate = x.TaxRate,
                Comment = x.Comment,
                Weight = x.Weight,
                Dimension = x.Dimension,
            }).ToList(),
            ProductImages = product.ProductImages.Select(x => new ProductImageDto
            {
                Id = x.Id,
                ThumbnailImageUrl = x.ThumbnailImageUrl,
                LargeImageUrl = x.LargeImageUrl
            }).ToList()
        };
    }
}