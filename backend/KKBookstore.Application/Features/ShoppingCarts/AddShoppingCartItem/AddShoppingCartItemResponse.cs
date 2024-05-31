using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

namespace KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;

public record AddShoppingCartItemResponse
{
    public AddShoppingCartItemResponse(ShoppingCartItem cartItem)
    {
        ProductId = cartItem.Sku.ProductId;
        SkuId = cartItem.SkuId;
        Quantity = cartItem.Quantity;
        UnitPrice = cartItem.Sku.UnitPrice;
    }

    public int ProductId { get; init; }
    public int SkuId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}
