using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ProductAggregate
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
