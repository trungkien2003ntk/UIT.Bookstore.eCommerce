using KKBookstore.Domain.Common;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ShoppingCart;

public class ShoppingCartItem : BaseAuditableEntity
{
    public ShoppingCartItem(int id): base(id)
    {
         
    }
    public int ShoppingCartItemId { get; set; }
    public int CustomerId { get; set; }
    public int SkuId { get; set; }
    public int Quantity { get; set; }


    // navigation properties
    public Sku Sku { get; set; }
    public User Customer { get; set; }
}
