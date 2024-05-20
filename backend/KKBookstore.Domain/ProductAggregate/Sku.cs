using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;

public class Sku : BaseAuditableEntity, ISoftDelete
{
    protected Sku() : base() { }

    public Sku(int productId) : base() 
    {
        ProductId = productId;
        Comment = "";
        Tags = "";
        IsActive = true;
        IsDeleted = false;
    }

    private Sku(
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
    public decimal TaxRate { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
    public DateTimeOffset? DiscontinuedWhen { get; set; }
    public int Quantity { get; set; }
    public int Weight { get; set; }
    public Dimension Dimension { get; set; }
    public bool IsActive { get; set; }
    public string Tags { get; set; }
    public SkuStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public ICollection<SkuOptionValue> SkuOptionValues { get; set; } = new List<SkuOptionValue>();
    public ICollection<SkuImage> SkuImages { get; set; } = new List<SkuImage>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public static Result<Sku> Create(
        int productId,
        decimal recommendedRetailPrice,
        decimal unitPrice,
        decimal taxRate,
        string comment,
        string tags,
        string barcode
    )
    {
        var skuValueResult = SkuValue.Create(productId);
        if (skuValueResult.IsFailure)
        {
            return Result.Failure<Sku>(skuValueResult.Error);
        }
        

        return new Sku(skuValueResult.Value, productId, recommendedRetailPrice, unitPrice, taxRate, comment, tags, barcode);
    }

}
