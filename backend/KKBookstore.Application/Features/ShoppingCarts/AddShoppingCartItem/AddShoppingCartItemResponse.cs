using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

namespace KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;

public record AddShoppingCartItemResponse
{
    public AddShoppingCartItemResponse(ShoppingCartItem cartItem)
    {
        ProductId = cartItem.ProductVariant.ProductId;
        ProductVariantId = cartItem.ProductVariantId;
        Quantity = cartItem.Quantity;
        UnitPrice = cartItem.ProductVariant.UnitPrice;
    }

    public int ProductId { get; init; }
    public int ProductVariantId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}
