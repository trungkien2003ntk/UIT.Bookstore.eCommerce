using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Products;

namespace KKBookstore.Application.Features.Products.CreateProduct;

public record CreateProductResponse : BaseDto
{
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public string Description { get; set; }
    public bool IsBook { get; set; }
    public bool IsActive { get; set; }
    public int UnitMeasureId { get; set; }

    // navigation properties
    public ProductTypeDto ProductType { get; set; }
    public UnitMeasureDto UnitMeasure { get; set; }
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = [];
    public ICollection<ProductImageDto> ProductImages { get; set; } = [];

    public sealed record ProductTypeDto : BaseDto
    {
        public string ProductTypeCode { get; set; }
        public int Level { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? ParentProductTypeId { get; set; }
    }

    public sealed record UnitMeasureDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed record ProductVariantDto : BaseDto
    {
        public decimal RecommendedRetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Weight { get; set; }
        public Dimension Dimension { get; set; }
        public decimal TaxRate { get; set; }
        public string Comment { get; set; }

        public ICollection<VariantOptionDto> VariantOptions { get; set; } = [];
    }

    public sealed record ProductImageDto : BaseDto
    {
        public string ThumbnailImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
    }

    public sealed record VariantOptionDto : BaseDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
