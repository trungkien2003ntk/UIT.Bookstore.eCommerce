using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Orders.Models;

namespace KKBookstore.Application.Features.Orders.GetOrderDetail;

public record GetOrderDetailResponse : BaseDto
{
    public string OrderNumber { get; init; }
    public DateTimeOffset? DueWhen { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public decimal Subtotal { get; init; }
    public decimal TaxRate { get; init; }
    public decimal Total => Subtotal * (1 + TaxRate);
    public decimal PaidAmount { get; init; }
    public string? Comment { get; init; }
    public string? DeliveryInstruction { get; init; }
    public int CustomerId { get; init; }
    public int? DiscountVoucherId { get; init; }
    public string Status { get; init; }
    public DateTimeOffset? PickingCompletedWhen { get; init; }
    public DateTimeOffset? ConfirmedDeliveryWhen { get; init; }
    public DateTimeOffset? ConfirmedReceivedWhen { get; init; }
    public DateTimeOffset OrderWhen { get; init; }
    public DeliveryMethodDto DeliveryMethod { get; init; }
    public PaymentMethodDto PaymentMethod { get; init; }
    public ShippingAddressDto ShippingAddress { get; init; }
    public List<OrderLineDto> OrderLines { get; init; } = [];
}
