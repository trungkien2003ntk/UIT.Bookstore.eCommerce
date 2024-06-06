namespace KKBookstore.API.Contracts.Requests;

public record ConfirmCheckoutRequest
{
    public List<int> ItemIds { get; init; } = [];
}
