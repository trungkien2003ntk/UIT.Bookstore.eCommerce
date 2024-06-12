namespace KKBookstore.API.Contracts.Requests;

public record ConfirmCheckoutRequest
{
    public List<int> ItemIds { get; init; } = [];
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingDiscountVoucherId { get; init; }
}
