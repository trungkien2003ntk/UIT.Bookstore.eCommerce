namespace KKBookstore.API.Contracts.Requests.ShoppingCart;

public class AddShoppingCartItemRequest
{
    public int SkuId { get; set; }
    public int Quantity { get; set; }
}
