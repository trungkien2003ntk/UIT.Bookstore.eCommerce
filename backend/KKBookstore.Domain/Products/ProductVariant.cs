using KKBookstore.Domain.Models;
using KKBookstore.Domain.Stocks;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Products;

public class ProductVariant : BaseFullAuditedEntity
{
    protected ProductVariant() : base() { }

    public ProductVariant(
        SkuValue skuValue,
        decimal recommendedRetailPrice,
        decimal unitPrice,
        int weight,
        Dimension dimension,
        decimal taxRate,
        string comment
    ) : base()
    {
        SkuValue = skuValue;
        Barcode = skuValue.Value;
        RecommendedRetailPrice = recommendedRetailPrice;
        Dimension = dimension;
        Weight = weight;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
        Comment = comment;
        Tags = "";
        ValidFrom = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public ProductVariant(int productId) : base()
    {
        ProductId = productId;
        Comment = "";
        Tags = "";
        IsActive = true;
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
        Barcode = SkuValue.Value;
        ProductId = productId;
        RecommendedRetailPrice = recommendedRetailPrice;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
        Comment = comment;
        Barcode = barcode;
        Tags = tags;
        ValidFrom = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public SkuValue SkuValue { get; set; } = null!;
    public string Barcode { get; set; }
    public int ProductId { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
    public DateTimeOffset? DiscontinuedWhen { get; set; }

    public int Weight { get; set; }
    public Dimension Dimension { get; set; }
    public bool IsActive { get; set; }
    public string Tags { get; set; }

    // Calculated properties
    [NotMapped]
    public decimal BasicDiscountRate => (RecommendedRetailPrice - UnitPrice) / RecommendedRetailPrice * 100;

    [NotMapped]
    public int StockQuantity => Inventories.Sum(i => i.StockQuantity);

    [NotMapped]
    public int AvailableQuantity => Inventories.Sum(i => i.IsActive ? i.StockQuantity : 0);

    // navigation properties
    public Product Product { get; set; }
    public ICollection<ProductVariantOptionValue> ProductVariantOptionValues { get; set; } = [];
    public ICollection<Rating> Ratings { get; set; } = [];
    /*
    This is a list of inventory.
    If we want to know the quantity, purchase price. We can use this.
    For the listing of purchase price in the create order page, we order this list by the creationTime, then select the first one
    based on the selected branch that the customer chose.
     */
    public ICollection<Inventory> Inventories { get; set; } = [];

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
