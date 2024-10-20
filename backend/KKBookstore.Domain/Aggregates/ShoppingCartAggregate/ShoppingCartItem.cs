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
        int productVariantId,
        int quantity
    )
    {
        CustomerId = customerId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
    }

    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    [NotMapped]
    public bool IsSelected { get; set; }
    [NotMapped]
    public decimal TotalUnitPrice => ProductVariant.UnitPrice * Quantity;
    [NotMapped]
    public decimal TotalRecommendedRetailPrice => ProductVariant.RecommendedRetailPrice * Quantity;
    [NotMapped]
    public decimal TotalSavedAmount => TotalRecommendedRetailPrice - TotalUnitPrice;

    // navigation properties
    public ProductVariant ProductVariant { get; set; }
    public User Customer { get; set; }

    public static Result<ShoppingCartItem> Create(
        int customerId,
        int productVariantId,
        int quantity
    )
    {
        if (quantity <= 0)
        {
            return Result.Failure<ShoppingCartItem>(ShoppingCartError.QuantityMustBePositive);
        }


        return new ShoppingCartItem(customerId, productVariantId, quantity);
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
