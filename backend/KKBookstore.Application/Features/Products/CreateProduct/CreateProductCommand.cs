using KKBookstore.Application.Common.Constants;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Branches;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.Products.Events;
using KKBookstore.Domain.ProductTypes;
using KKBookstore.Domain.Stocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Application.Features.Products.CreateProduct;

public record CreateProductCommand : IRequest<Result<AdminProductDto>>
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int ProductTypeId { get; set; }
    public string? Description { get; set; }
    public bool IsBook { get; set; } = false;

    [Required]
    public bool IsActive { get; set; }

    public int? UnitMeasureId { get; set; }

    [Required]
    public ICollection<AttributeProductValueCreateDto> AttributeProductValues { get; set; } = null!;
    public ICollection<AuthorDto>? BookAuthors { get; set; }
    [Required]
    public ICollection<ProductVariantCreateDto> ProductVariants { get; set; } = null!;
    public ICollection<ProductImageCreateDto>? ProductImages { get; set; }

    public class AuthorDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class AttributeProductValueCreateDto
    {
        [Required]
        public int AttributeId { get; set; }
        [Required]
        public string Value { get; set; } = null!;
    }

    public class ProductVariantCreateDto
    {
        public decimal RecommendedRetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Weight { get; set; }
        public Dimension Dimension { get; set; } = null!;
        public decimal TaxRate { get; set; }
        public string? Comment { get; set; }

        public ICollection<VariantOptionCreateDto>? VariantOptions { get; set; }
    }

    public class ProductImageCreateDto
    {
        [Required]
        public string ThumbnailImageUrl { get; set; } = null!;

        [Required]
        public string LargeImageUrl { get; set; } = null!;
    }

    public class VariantOptionCreateDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<AdminProductDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IApplicationDbContext dbContext, IServiceBus serviceBus, ILogger<CreateProductCommandHandler> logger)
    {
        _dbContext = dbContext;
        _serviceBus = serviceBus;
        _logger = logger;
    }

    public async Task<Result<AdminProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            // 1. Validate product type exists first
            var productType = await _dbContext.ProductTypes.FindAsync(request.ProductTypeId);
            if (productType == null)
            {
                return Result.Failure<AdminProductDto>(ProductTypeErrors.NotFound);
            }

            // 2. Get unit measure
            var unitMeasure = (await _dbContext.UnitMeasures.FindAsync(request.UnitMeasureId ?? 1))!;

            // 3. Get all branches for inventory creation
            var branches = await _dbContext.Branches.ToListAsync(cancellationToken);


            var productExists = await _dbContext.Products.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (productExists)
            {
                return Result.Failure<AdminProductDto>(ProductErrors.ProductAlreadyExists(request.Name));
            }

            // 4. Create main product entity first
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description ?? "",
                IsActive = request.IsActive,
                IsBook = request.IsBook,
                ProductTypeId = productType.Id,
                UnitMeasureId = unitMeasure.Id
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var createAttributeValuesResult = await CreateAttributeValuesAsync(request.AttributeProductValues, product);
            if (createAttributeValuesResult.IsFailure)
            {
                return Result.Failure<AdminProductDto>(createAttributeValuesResult.Error);
            }
            var attributeValues = createAttributeValuesResult.Value;
            _dbContext.ProductTypeAttributeProductValues.AddRange(attributeValues);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Attribute values created for product {ProductId}", product.Id);

            // 5. Create and save product options
            var productOptions = CreateProductOptions(request, product);
            _dbContext.ProductOptions.AddRange(productOptions);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Product options created for product {ProductId}", product.Id);

            // 6. Create and save product variants with their options
            var variants = await CreateProductVariants(request, product, productOptions, branches);
            _dbContext.ProductVariants.AddRange(variants);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Product variants created for product {ProductId}", product.Id);

            // 7. Create and save product images if any
            if (request.ProductImages != null)
            {
                var productImages = CreateProductImages(request, product);
                _dbContext.ProductImages.AddRange(productImages);
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Product images created for product {ProductId}", product.Id);
            }

            _logger.LogInformation("Sending product created event for product {ProductId}", product.Id);
            var productCreatedEvent = new ProductCreatedEvent(
                product.Id,
                product.ProductImages.Select(i => new ImageDto(i.Id, i.ThumbnailImageUrl)));
            await _serviceBus.SendMessageAsync(productCreatedEvent, ServiceBusConsts.ProductCreatedQueueName);



            await transaction.CommitAsync(cancellationToken);
            return CreateResponse(product, productType, unitMeasure);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Create product failed");
            return Result.Failure<AdminProductDto>(ProductErrors.CreateProductFailed);
        }
    }

    private async Task<Result<List<ProductTypeAttributeProductValue>>> CreateAttributeValuesAsync(
        ICollection<CreateProductCommand.AttributeProductValueCreateDto> attributeProductValues,
        Product product)
    {
        var attributeIds = attributeProductValues.Select(apv => apv.AttributeId).ToList();
        // Fetch all matching ProductTypeAttributes in one query
        var productTypeAttributes = await _dbContext.ProductTypeAttributes
            .Where(pta => attributeIds.Contains(pta.Id))
            .Include(pta => pta.Values)
            .ToListAsync();

        var existingIds = productTypeAttributes.Select(pta => pta.Id).ToHashSet();
        var missingIds = attributeIds.Except(existingIds).ToList();
        if (missingIds.Count != 0)
        {
            return Result.Failure<List<ProductTypeAttributeProductValue>>(ProductErrors.AttributeNotFound(missingIds));
        }

        var existingValues = productTypeAttributes.SelectMany(pta => pta.Values).ToList();

        return attributeProductValues.Select(apv =>
        {
            var existingValue = existingValues.FirstOrDefault(v => v.Value == apv.Value && v.ProductTypeAttributeId == apv.AttributeId);

            return new ProductTypeAttributeProductValue
            {
                ProductId = product.Id,
                Product = product,
                AttributeValueId = existingValue?.Id ?? 0,
                AttributeValue = existingValue ?? new()
                {
                    ProductTypeAttributeId = apv.AttributeId,
                    ProductTypeAttribute = productTypeAttributes.First(a => a.Id == apv.AttributeId),
                    Value = apv.Value
                }
            };
        }).ToList();
    }

    private List<ProductOption> CreateProductOptions(CreateProductCommand request, Product product)
    {
        var productOptions = request.ProductVariants
            .Where(v => v.VariantOptions is not null)
            .SelectMany(v => v.VariantOptions!)
            .GroupBy(vo => vo.Name)
            .Select(group => new ProductOption
            {
                Name = group.Key,
                ProductId = product.Id,
                Product = product
            })
            .ToList();

        return productOptions;
    }

    private async Task<List<ProductVariant>> CreateProductVariants(
        CreateProductCommand request,
        Product product,
        List<ProductOption> savedOptions,
        List<Branch> branches)
    {
        var variants = new List<ProductVariant>();

        if (request.ProductVariants.Count == 1)
        {
            var variantRequest = request.ProductVariants.First();
            variants.Add(new ProductVariant(
                new SkuValue(),
                variantRequest.RecommendedRetailPrice,
                variantRequest.UnitPrice,
                variantRequest.Weight,
                variantRequest.Dimension,
                variantRequest.TaxRate,
                variantRequest.Comment ?? "")
            {
                ProductId = product.Id
            });
        }
        else
        {
            foreach (var variantRequest in request.ProductVariants)
            {
                var variant = new ProductVariant(
                    new SkuValue(),
                    variantRequest.RecommendedRetailPrice,
                    variantRequest.UnitPrice,
                    variantRequest.Weight,
                    variantRequest.Dimension,
                    variantRequest.TaxRate,
                    variantRequest.Comment ?? "")
                {
                    ProductId = product.Id
                };

                // Add variant options
                if (variantRequest.VariantOptions != null)
                {
                    foreach (var optionRequest in variantRequest.VariantOptions)
                    {
                        var option = savedOptions.First(o => o.Name == optionRequest.Name);
                        var optionValue = new ProductOptionValue { Value = optionRequest.Value, OptionId = option.Id };

                        variant.ProductVariantOptionValues.Add(new ProductVariantOptionValue
                        {
                            OptionId = option.Id,
                            Option = option,
                            OptionValue = optionValue,
                            ProductVariant = variant
                        });
                    }
                }

                // Add inventories for each branch
                foreach (var branch in branches)
                {
                    variant.Inventories.Add(new Inventory
                    {
                        WarehouseId = branch.Id,
                        PurchaseOrderLineId = null,
                        StockQuantity = 0,
                        PurchasePrice = 0,
                        IsActive = true
                    });
                }

                variants.Add(variant);
            }
        }

        return variants;
    }

    private List<ProductImage> CreateProductImages(CreateProductCommand request, Product product)
    {
        return request.ProductImages!.Select(image => new ProductImage
        {
            ProductId = product.Id,
            ThumbnailImageUrl = image.ThumbnailImageUrl,
            LargeImageUrl = image.LargeImageUrl
        }).ToList();
    }

    private AdminProductDto CreateResponse(Product product, ProductType productType, UnitMeasure unitMeasure)
    {
        return new AdminProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            IsActive = product.IsActive,
            IsBook = product.IsBook,
            ProductTypeId = product.ProductTypeId,
            UnitMeasureId = product.UnitMeasureId,
            ProductType = new ProductTypeDto
            {
                Id = productType.Id,
                ProductTypeCode = productType.ProductTypeCode,
                Level = productType.Level,
                DisplayName = productType.DisplayName,
                Description = productType.Description,
                ParentProductTypeId = productType.ParentProductTypeId
            },
            UnitMeasure = new UnitMeasureDto
            {
                Id = unitMeasure.Id,
                Name = unitMeasure.Name,
                Description = unitMeasure.Description
            },
            ProductVariants = product.ProductVariants.Select(v => new ProductVariantDto
            {
                Id = v.Id,
                RecommendedRetailPrice = v.RecommendedRetailPrice,
                UnitPrice = v.UnitPrice,
                Weight = v.Weight,
                Dimension = v.Dimension,
                TaxRate = v.TaxRate,
                Comment = v.Comment,
                VariantOptions = v.ProductVariantOptionValues.Select(vo => new ProductVariantDto.VariantOptionDto
                {
                    ProductOptionId = vo.OptionId,
                    ProductOptionValueId = vo.OptionValueId,
                    Name = vo.Option.Name,
                    Value = vo.OptionValue.Value
                }).ToList()
            }).ToList(),
            ProductImages = product.ProductImages.Select(pi => new ProductImageDto
            {
                Id = pi.Id,
                ThumbnailImageUrl = pi.ThumbnailImageUrl,
                LargeImageUrl = pi.LargeImageUrl
            }).ToList(),
            AttributeProductValues = product.AttributeProductValues.Select(pav => new ProductTypeAttributeProductValueDto
            {
                AttributeId = pav.AttributeValue.ProductTypeAttribute.Id,
                AttributeValueId = pav.AttributeValueId,
                Name = pav.AttributeValue.ProductTypeAttribute.Name,
                Value = pav.AttributeValue.Value
            }).ToList()
        };
    }
}