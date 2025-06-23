using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Features.Orders.Models;

namespace KKBookstore.Features.Orders.GetOrderList;

public record OrderGeneralInformation : BaseAuditedDto
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
    public int? ShippingAddressId { get; init; }
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

    // Customer Information
    public string CustomerFullName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public string CustomerPhoneNumber { get; init; } = string.Empty;
    public string CustomerAvartarUrl { get; init; } = string.Empty;

    // Shipping Address Information  
    public string ShippingReceiverName { get; init; } = string.Empty;
    public string ShippingPhoneNumber { get; init; } = string.Empty;
    public string ShippingDetailedAddress { get; init; } = string.Empty;
    public string ShippingProvinceName { get; init; } = string.Empty;
    public string ShippingDistrictName { get; init; } = string.Empty;
    public string ShippingCommuneName { get; init; } = string.Empty;
}
