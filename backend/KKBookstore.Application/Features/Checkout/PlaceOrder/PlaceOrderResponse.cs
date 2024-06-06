namespace KKBookstore.Application.Features.Checkout.PlaceOrder;

public record PlaceOrderResponse
{
    public int OrderId { get; set; }
    public string PaymentUrl { get; set; }
}