namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record GetShoppingCartResponse
{
    public List<ShoppingCartItemDto> Items { get; init; } = [];

    public GetShoppingCartResponse()
    {
    }
}
