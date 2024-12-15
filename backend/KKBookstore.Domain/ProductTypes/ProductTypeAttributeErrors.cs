using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.ProductTypes;

public static class ProductTypeAttributeErrors
{
    public static readonly Error NotFound = Error.NotFound("ProductType.NotFound", "Product type not found");
    public static readonly Error ValueNotFound = Error.NotFound("ProductType.ValueNotFound", "Product type value not found");
    public static readonly Error ValueInUse = Error.Conflict("ProductType.ValueInUse", "Product type value is in use");
}