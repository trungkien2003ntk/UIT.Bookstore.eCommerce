namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

internal sealed class TempCartItem
{
    public int Id { get; set; }
    public int SkuId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset CreatedWhen { get; set; }
    public int ProductId { get; set; }
}
