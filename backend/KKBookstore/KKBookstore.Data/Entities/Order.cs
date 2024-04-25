using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Data.Entities
{
    public class Order : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  OrderId { get; set; }

        public int? CustomerId { get; set; }

        public int? DeliveryId { get; set; }

        public int? PaymentId { get; set; }

        public Guid PaymentCode { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? Total { get; set; }

        // Implement Discount system
    }
}
