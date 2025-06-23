using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Orders.Models;

public record OrderLineDto : BaseAuditedDto
{
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public int? ProductVariantId { get; init; }

    // Product Information
    public required string ProductName { get; init; }
    public string? ProductDescription { get; init; }
    public required string ProductTypeName { get; init; }
    public string? ProductVariantName { get; init; }

    // Pricing Information
    public decimal UnitPrice { get; init; }
    public decimal? RecommendedRetailPrice { get; init; }
    public int Quantity { get; init; }
    public decimal Total => UnitPrice * Quantity;
    public decimal DiscountAmount { get; init; }
    public bool Rated { get; init; }
    public decimal FinalPrice => Total - DiscountAmount;

    // Product Images
    public required string ThumbnailUrl { get; init; }
    public string? LargeImageUrl { get; init; }

    // Product Variant Options (formatted for display)
    public IEnumerable<ProductOptionDto> VariantOptions { get; init; } = [];

    // Product Attributes
    public IEnumerable<ProductAttributeDto> ProductAttributes { get; init; } = [];

    public sealed record ProductOptionDto
    {
        public required string OptionName { get; init; }
        public required string OptionValue { get; init; }
    }

    public sealed record ProductAttributeDto
    {
        public required string AttributeName { get; init; }
        public required string AttributeValue { get; init; }
    }
}