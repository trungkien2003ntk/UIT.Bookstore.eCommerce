using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductTypeAggregate;

public static class ProductTypeErrors
{
    //not found
    public static readonly Error NotFound = Error.NotFound("ProductType.NotFound", "Product type not found");
}
