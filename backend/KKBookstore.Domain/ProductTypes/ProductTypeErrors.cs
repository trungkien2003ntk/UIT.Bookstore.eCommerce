using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.ProductTypes;

public static class ProductTypeErrors
{
    //not found
    public static readonly Error NotFound = Error.NotFound("ProductType.NotFound", "Product type not found");
    public static readonly Error AttributeAlreadyAssociated = Error.Conflict("ProductType.AttributeAlreadyAssociated", "Attribute is already associated with the product type");
}
