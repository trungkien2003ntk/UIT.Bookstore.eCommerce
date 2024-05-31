using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public record ShoppingCartUpdateSummary
{
    public List<ShoppingCartItemDto> UpdatedItems { get; init; } = [];
    public decimal TotalPrice { get; init; }
    public DiscountDetailDto DiscountDetail { get; init; }
}