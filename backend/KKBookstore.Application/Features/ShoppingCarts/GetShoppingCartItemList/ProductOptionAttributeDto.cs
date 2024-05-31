namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record ProductOptionAttributeDto
{
    public string Name { get; init; }
    public IEnumerable<string> Values { get; init; }
    public IEnumerable<string> Images { get; init; }
}
