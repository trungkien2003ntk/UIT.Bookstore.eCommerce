using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class ProductPriceHistory : BaseAuditedEntity
{
    public int ProductVariantId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }

    // navigation properties
    public ProductVariant ProductVariant { get; set; } = null!;
}
