using KKBookstore.Models;

namespace KKBookstore.Products;

public static class ProductOptionErrors
{
    public static readonly Error NotFound = Error.NotFound("ProductOption.NotFound", "Product option was not found.");
}