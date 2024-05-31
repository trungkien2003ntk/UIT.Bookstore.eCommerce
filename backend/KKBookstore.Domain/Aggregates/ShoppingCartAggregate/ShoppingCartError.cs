using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

public static class ShoppingCartError
{
    public static readonly Error QuantityMustBePositive = Error.BusinessRuleViolation("ShoppingCartError.QuantityMustBePositive", "Quantity must be greater than 0");
    public static readonly Error InvalidSkuToAdd = Error.Validation("ShoppingCartError.InvalidSkuToAdd", "Invalid SKU to add to shopping cart");

    public static readonly Error Unknown = Error.Failure("ShoppingCartError.Unknown", "An unknown error occurred");
}
