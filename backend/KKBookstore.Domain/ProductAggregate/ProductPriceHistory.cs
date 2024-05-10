using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate
{
    public class ProductPriceHistory : ITrackable
    {
        public int SkuId { get; set; }
        public DateTimeOffset StartWhen { get; set; }
        public DateTimeOffset EndWhen { get; set; }
        public decimal RecommendedRetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int LastEditedBy { get; set; }
        public DateTimeOffset LastEditedWhen { get; set; }

        // navigation properties
        public User LastEditedByUser { get; set; }
        public Sku Sku { get; set; }
    }
}
