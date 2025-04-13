namespace KKBookstore.Contracts.Requests.ShoppingCart;

public class AddShoppingCartItemRequest
{
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
