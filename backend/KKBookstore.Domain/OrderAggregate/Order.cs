using KKBookstore.Domain.Common;
using KKBookstore.Domain.DiscountAggregate;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.OrderAggregate
{

    //OrderId
    //CustomerId
    //DueWhen
    //ExpectedDeliveryWhen
    //OrderNumber
    //ShippingAddressId
    //Subtotal
    //TaxAmount
    //Comment
    //DeliveryInstruction
    //DiscountId
    //PaymentMethod
    //Status
    //PickingCompletedWhen
    //OrderWhen
    //LastEditedBy
    //LastEditedWhen
    public class Order : BaseAuditableEntity
    {
        private Order(
            int customerId,
            int shippingAddressId,
            int deliveryMethodId,
            int paymentMethodId,
            DateTimeOffset orderWhen,
            decimal taxRate,
            OrderStatus status
        ) : base()
        {
            OrderNumber = GenerateOrderNumber();
            CustomerId = customerId;
            ShippingAddressId = shippingAddressId;
            DeliveryMethodId = deliveryMethodId;
            PaymentMethodId = paymentMethodId;
            OrderWhen = orderWhen;
            TaxRate = taxRate;
            Status = status;
        }

        public string OrderNumber { get; private set; }
        public DateTimeOffset? DueWhen { get; set; }
        public DateTimeOffset ExpectedDeliveryWhen { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal TaxRate { get; set; }
        public string? Comment { get; set; }
        public string? DeliveryInstruction { get; set; }
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? DiscountVoucherId { get; set; }
        public int PaymentMethodId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTimeOffset? PickingCompletedWhen { get; set; }
        public DateTimeOffset? ConfirmedDeliveryWhen { get; set; }
        public DateTimeOffset? ConfirmedReceivedWhen { get; set; }
        public DateTimeOffset OrderWhen { get; set; }

        // navigation properties
        public ShippingAddress ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public DiscountVoucher? DiscountVoucher { get; set; }
        public User Customer { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        private string GenerateOrderNumber()
        {
            // generate order number follow pattern year-month-day/short_uid
            return $"{OrderWhen.Year}-{OrderWhen.Month}-{OrderWhen.Day}/{Guid.NewGuid().ToString()[..5]}";
        }

        public static Result<Order> Create(
            int customerId,
            int shippingAddressId,
            int deliveryMethodId,
            int paymentMethodId,
            DateTimeOffset orderWhen,
            decimal taxRate,
            OrderStatus status
        )
        {

            return new Order(
                customerId,
                shippingAddressId,
                deliveryMethodId,
                paymentMethodId,
                orderWhen,
                taxRate,
                status
            );
        }
    }
}
