namespace KKBookstore.Features.Checkout.HandleIPN;

public record HandleIPNResponse
{
    public string RspCode { get; set; }
    public string Message { get; set; }
}