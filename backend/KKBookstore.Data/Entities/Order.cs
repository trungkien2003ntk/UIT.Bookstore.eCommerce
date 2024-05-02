using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Data.Entities
{

    //OrderID
    //CustomerID
    //DueWhen
    //ExpectedDeliveryWhen
    //OrderNumber
    //ShippingAddressID
    //Subtotal
    //TaxAmount
    //Comment
    //DeliveryInstruction
    //DiscountID
    //PaymentMethod
    //Status
    //PickingCompletedWhen
    //OrderWhen
    //LastEditedBy
    //LastEditedWhen
    public class Order : BaseEntity, ITrackable
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTimeOffset? DueWhen { get; set; }
        public DateTimeOffset ExpectedDeliveryWhen { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal TaxRate { get; set; }
        public string? Comment { get; set; }
        public string? DeliveryInstruction { get; set; }
        public int CustomerID { get; set; }
        public int ShippingAddressID { get; set; }
        public int DeliveryMethodID { get; set; }
        public int? DiscountVoucherID { get; set; }
        public int PaymentMethodID  { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? PickingCompletedWhen { get; set; }
        public DateTimeOffset? ConfirmedDeliveryWhen { get; set; }
        public DateTimeOffset? ConfirmedReceivedWhen { get; set; }
        public DateTimeOffset OrderWhen { get; set; }
        public int LastEditedBy { get; set; }
        public DateTimeOffset LastEditedWhen { get; set; }

        // navigation properties
        public ShippingAddress ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public DiscountVoucher? DiscountVoucher { get; set; }
        public User Customer { get; set; }
        public User LastEditedByUser { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
