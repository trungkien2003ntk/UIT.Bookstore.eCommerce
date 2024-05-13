using KKBookstore.Domain.Common;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ShoppingCart;

public class ShoppingCartItem : BaseAuditableEntity
{
    private ShoppingCartItem(
        int customerId,
        int skuId,
        int quantity
    )
    {
        CustomerId = customerId;
        SkuId = skuId;
        Quantity = quantity;
    }

    public int ShoppingCartItemId { get; set; }
    public int CustomerId { get; set; }
    public int SkuId { get; set; }
    public int Quantity { get; set; }


    // navigation properties
    public Sku Sku { get; set; }
    public User Customer { get; set; }

    public static Result<ShoppingCartItem> Create(
        int customerId,
        int skuId,
        int quantity
    )
    {
        if (quantity <= 0)
        {
            return Result.Failure<ShoppingCartItem>(ShoppingCartError.QuantityMustBePositive);
        }


        return new ShoppingCartItem(customerId, skuId, quantity);
    }
}
