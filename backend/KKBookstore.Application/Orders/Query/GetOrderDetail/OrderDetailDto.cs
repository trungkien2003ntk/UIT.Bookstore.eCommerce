using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.OrderAggregate;

namespace KKBookstore.Application.Orders.Query.GetOrderDetail
{
    public record OrderDetailDto : BaseDto
    {
        public string OrderNumber { get; init; }
        public DateTimeOffset? DueWhen { get; init; }
        public DateTimeOffset ExpectedDeliveryWhen { get; init; }
        public decimal? Subtotal { get; init; }
        public decimal TaxRate { get; init; }
        public string? Comment { get; init; }
        public string? DeliveryInstruction { get; init; }
        public int CustomerId { get; init; }
        public int ShippingAddressId { get; init; }
        public int DeliveryMethodId { get; init; }
        public int? DiscountVoucherId { get; init; }
        public int PaymentMethodId { get; init; }
        public OrderStatus Status { get; init; }
        public DateTimeOffset? PickingCompletedWhen { get; init; }
        public DateTimeOffset? ConfirmedDeliveryWhen { get; init; }
        public DateTimeOffset? ConfirmedReceivedWhen { get; init; }
        public DateTimeOffset OrderWhen { get; init; }
        public ICollection<OrderLine> OrderLines { get; init; } = new List<OrderLine>();
    }
}