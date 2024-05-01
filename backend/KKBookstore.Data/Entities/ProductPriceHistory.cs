
namespace KKBookstore.Data.Entities
{
    public class ProductPriceHistory : ITrackable
    {
        public int SkuID { get; set; }
        public DateTimeOffset StartWhen { get; set; }
        public DateTimeOffset EndWhen { get; set; }
        public decimal RecommendedRetailPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int LastEditedBy { get; set; }
        public User LastEditedByUser { get; set; }
        public DateTimeOffset LastEditedWhen { get; set; }
    }
}
