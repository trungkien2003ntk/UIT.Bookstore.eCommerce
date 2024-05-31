using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

public class ShoppingCartItem : BaseAuditableEntity
{
    public ShoppingCartItem() { }
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

    public int CustomerId { get; set; }
    public int SkuId { get; set; }
    public int Quantity { get; set; }
    [NotMapped]
    public bool IsSelected { get; set; }

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

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SubtractQuantity(int quantity)
    {
        Quantity -= quantity;
    }

    public decimal GetTotalUnitPrice()
    {
        return Sku.UnitPrice * Quantity;
    }

    public decimal GetTotalRecommendedRetailPrice()
    {
        return Sku.RecommendedRetailPrice * Quantity;
    }

    public decimal GetTotalSavedAmount()
    {
        return GetTotalRecommendedRetailPrice() - GetTotalUnitPrice();
    }
}
