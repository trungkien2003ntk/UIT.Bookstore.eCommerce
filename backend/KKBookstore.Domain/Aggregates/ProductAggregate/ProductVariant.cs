using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class ProductVariant : BaseAuditableEntity, ISoftDelete
{
    protected ProductVariant() : base() { }

    public ProductVariant(int productId) : base()
    {
        ProductId = productId;
        Comment = "";
        Tags = "";
        IsActive = true;
        IsDeleted = false;
    }

    private ProductVariant(
        SkuValue skuValue,
        int productId,
        decimal recommendedRetailPrice,
        decimal unitPrice,
        decimal taxRate,
        string comment,
        string tags,
        string barcode
    ) : base()
    {
        SkuValue = skuValue;
        ProductId = productId;
        RecommendedRetailPrice = recommendedRetailPrice;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
        Comment = comment;
        Barcode = barcode;
        Tags = tags;
        ValidFrom = DateTimeOffset.UtcNow;
        IsActive = true;
        IsDeleted = false;
    }

    public SkuValue SkuValue { get; set; }
    public string Barcode { get; set; }
    public int ProductId { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }
    [NotMapped]
    public decimal BasicDiscountRate => (RecommendedRetailPrice - UnitPrice) / RecommendedRetailPrice * 100;
    public decimal TaxRate { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
    public DateTimeOffset? DiscontinuedWhen { get; set; }
    public int Quantity { get; set; }
    public int AvailableQuantity => Quantity;
    public int Weight { get; set; }
    public Dimension Dimension { get; set; }
    public bool IsActive { get; set; }
    public string Tags { get; set; }
    public InventoryStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public ICollection<ProductVariantOptionValue> ProductVariantOptionValues { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];

    // calculated properties
    public string VariantName => string.Join(", ", ProductVariantOptionValues.Select(sov => sov.OptionValue.Value));

    public string? GetThumbnailImageUrl()
    {
        var thumbnailImageUrl = ProductVariantOptionValues.FirstOrDefault(sov => !string.IsNullOrEmpty(sov.OptionValue.ThumbnailImageUrl))?.OptionValue.ThumbnailImageUrl;

        return thumbnailImageUrl;
    }

    public string? GetLargeImageUrl()
    {
        var largeImageUrl = ProductVariantOptionValues.FirstOrDefault(sov => !string.IsNullOrEmpty(sov.OptionValue.LargeImageUrl))?.OptionValue.LargeImageUrl;

        return largeImageUrl;
    }
}
