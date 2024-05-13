using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.ShoppingCart;

public static class ShoppingCartError
{
    public static readonly Error QuantityMustBePositive = Error.BusinessRuleViolation("ShoppingCartError.QuantityMustBePositive", "Quantity must be greater than 0");

}
