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
    [NotMapped]
    public decimal TotalUnitPrice => Sku.UnitPrice * Quantity;
    [NotMapped]
    public decimal TotalRecommendedRetailPrice => Sku.RecommendedRetailPrice * Quantity;
    [NotMapped]
    public decimal TotalSavedAmount => TotalRecommendedRetailPrice - TotalUnitPrice;

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

    public Result UpdateQuantity(int newQuantity)
    {
        // validation
        if (newQuantity <= 0)
        {
            return Result.Failure(ShoppingCartError.QuantityMustBePositive);
        }
        
        Quantity = newQuantity;
        return Result.Success();
    }
}
