using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate
{
    public class ProductPriceHistory : BaseAuditableEntity
    {
        public int SkuId { get; set; }
        public DateTimeOffset StartWhen { get; set; }
        public DateTimeOffset EndWhen { get; set; }
        public decimal RecommendedRetailPrice { get; set; }
        public decimal UnitPrice { get; set; }

        // navigation properties
        public Sku Sku { get; set; }
    }
}
