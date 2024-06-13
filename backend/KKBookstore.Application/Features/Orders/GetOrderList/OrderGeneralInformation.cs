using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Orders.Models;

namespace KKBookstore.Application.Features.Orders.GetOrderList;

public record OrderGeneralInformation : BaseDto
{
    public string OrderNumber { get; init; }
    public DateTimeOffset? DueWhen { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public decimal? Subtotal { get; init; }
    public decimal Total { get; init; }
    public decimal TaxRate { get; init; }
    public string? Comment { get; init; }
    public string? DeliveryInstruction { get; init; }
    public int CustomerId { get; init; }
    public int ShippingAddressId { get; init; }
    public string DeliveryMethodName { get; init; }
    public int? DiscountVoucherId { get; init; }
    public int? ShippingVoucherId { get; init; }
    public string PaymentMethodName { get; init; }
    public string Status { get; init; }
    public DateTimeOffset? PickingCompletedWhen { get; init; }
    public DateTimeOffset? ConfirmedDeliveryWhen { get; init; }
    public DateTimeOffset? ConfirmedReceivedWhen { get; init; }
    public DateTimeOffset OrderWhen { get; init; }
    public List<OrderLineDto> OrderLines { get; init; } = [];

}
