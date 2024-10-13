using KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

namespace KKBookstore.API.Contracts.Requests.ShoppingCart;

public class UpdateShoppingCartItemRequest
{
    public UpdateCartActionType ActionType { get; init; }
    public List<int> SelectedItemIds { get; init; } = [];
    public List<UpdateShoppingCartItemBriefDto> UpdateItems { get; init; } = [];
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingDiscountVoucherId { get; init; }

    public sealed record UpdateShoppingCartItemBriefDto
    {
        public int Id { get; init; }
        public int SkuId { get; init; }
        public int OldSkuId { get; init; }
        public int Quantity { get; init; }
        public int OldQuantity { get; init; }
    }
}
