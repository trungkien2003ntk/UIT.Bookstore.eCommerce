using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem
{
    public record UpdateShoppingCartItemBriefDto
    {
        public int Id { get; init; }
        public int SkuId { get; init; }
        public int OldSkuId { get; init; }
        public int Quantity { get; init; }
        public int OldQuantity { get; init; }

        //ToEntity
        public ShoppingCartItem ToEntity()
        {
            return new ShoppingCartItem
            {
                Id = Id,
                SkuId = SkuId,
                Quantity = Quantity
            };
        }
    }
}
