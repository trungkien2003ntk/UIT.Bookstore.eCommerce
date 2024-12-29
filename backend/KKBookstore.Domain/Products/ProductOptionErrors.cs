using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public static class ProductOptionErrors
{
    public static readonly Error NotFound = Error.NotFound("ProductOption.NotFound", "Product option was not found.");
}