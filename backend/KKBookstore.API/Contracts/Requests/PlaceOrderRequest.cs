namespace KKBookstore.API.Contracts.Requests;

public record PlaceOrderRequest
{
    public List<int> ItemIds { get; init; } = [];
    public int ShippingAddressId { get; init; }
    public int PaymentMethodId { get; init; }
    public int DeliveryMethodId { get; init; }
    public decimal ShippingFee { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingVoucherId { get; init; }
    public string Note { get; init; }

    public string? PaymentReturnUrl { get; init; }
}
