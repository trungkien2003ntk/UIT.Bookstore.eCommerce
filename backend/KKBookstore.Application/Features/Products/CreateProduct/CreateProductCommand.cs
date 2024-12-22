using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Branches;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ProductTypes;
using KKBookstore.Domain.Stocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Application.Features.Products.CreateProduct;

public record CreateProductCommand : IRequest<Result<CreateProductResponse>>
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

    public ICollection<AuthorDto>? BookAuthors { get; set; }
    [Required]
    public ICollection<ProductVariantCreateDto> ProductVariants { get; set; } = null!;
    public ICollection<ProductImageCreateDto>? ProductImages { get; set; }

    public class AuthorDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
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

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            // 1. Validate product type exists first
            var productType = await _dbContext.ProductTypes.FindAsync(request.ProductTypeId);
            if (productType == null)
            {
                return Result.Failure<CreateProductResponse>(ProductTypeErrors.NotFound);
            }

            // 2. Get unit measure
            var unitMeasure = (await _dbContext.UnitMeasures.FindAsync(request.UnitMeasureId ?? 1))!;

            // 3. Get all branches for inventory creation
            var branches = await _dbContext.Branches.ToListAsync(cancellationToken);


            var productExists = await _dbContext.Products.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (productExists)
            {
                return Result.Failure<CreateProductResponse>(ProductErrors.ProductAlreadyExists(request.Name));
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

            // 5. Create and save product options
            var productOptions = CreateProductOptions(request, product);
            _dbContext.ProductOptions.AddRange(productOptions);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 6. Create and save product variants with their options
            var variants = await CreateProductVariants(request, product, productOptions, branches);
            _dbContext.ProductVariants.AddRange(variants);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 7. Create and save product images if any
            if (request.ProductImages != null)
            {
                var productImages = CreateProductImages(request, product);
                _dbContext.ProductImages.AddRange(productImages);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            return CreateResponse(product, productType, unitMeasure);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<CreateProductResponse>(ProductErrors.CreateProductFailed);
        }
    }

    private List<ProductOption> CreateProductOptions(CreateProductCommand request, Product product)
    {
        return request.ProductVariants
            .Where(v => v.VariantOptions is not null)
            .SelectMany(v => v.VariantOptions!)
            .GroupBy(vo => vo.Name)
            .Select(group => new ProductOption
            {
                Name = group.Key,
                ProductId = product.Id
            })
            .ToList();
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

    private CreateProductResponse CreateResponse(Product product, ProductType productType, UnitMeasure unitMeasure)
    {
        return new CreateProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            IsActive = product.IsActive,
            IsBook = product.IsBook,
            ProductTypeId = product.ProductTypeId,
            UnitMeasureId = product.UnitMeasureId,
            ProductType = new CreateProductResponse.ProductTypeDto
            {
                Id = productType.Id,
                ProductTypeCode = productType.ProductTypeCode,
                Level = productType.Level,
                DisplayName = productType.DisplayName,
                Description = productType.Description,
                ParentProductTypeId = productType.ParentProductTypeId
            },
            UnitMeasure = new CreateProductResponse.UnitMeasureDto
            {
                Id = unitMeasure.Id,
                Name = unitMeasure.Name,
                Description = unitMeasure.Description
            },
            ProductVariants = product.ProductVariants.Select(v => new CreateProductResponse.ProductVariantDto
            {
                Id = v.Id,
                RecommendedRetailPrice = v.RecommendedRetailPrice,
                UnitPrice = v.UnitPrice,
                Weight = v.Weight,
                Dimension = v.Dimension,
                TaxRate = v.TaxRate,
                Comment = v.Comment,
                VariantOptions = v.ProductVariantOptionValues.Select(vo => new CreateProductResponse.VariantOptionDto
                {
                    Name = vo.Option.Name,
                    Value = vo.OptionValue.Value
                }).ToList()
            }).ToList()
        };
    }
}