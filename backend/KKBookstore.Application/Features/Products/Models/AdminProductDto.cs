using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.Models;

public record AdminProductDto : BaseDto
{
    public string Name { get; set; } = null!;
    public int ProductTypeId { get; set; }
    public string? Description { get; set; }
    public bool IsBook { get; set; }
    public bool IsActive { get; set; }
    public int UnitMeasureId { get; set; } = 1;

    // navigation properties
    public ProductTypeDto? ProductType { get; set; }
    public UnitMeasureDto? UnitMeasure { get; set; }
    public ICollection<ProductTypeAttributeProductValueDto> AttributeProductValues { get; set; } = null!;
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = [];
    public ICollection<ProductImageDto> ProductImages { get; set; } = [];
}