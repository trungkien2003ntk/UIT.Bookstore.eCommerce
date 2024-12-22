


namespace KKBookstore.Domain.Shared.Products;

public static class ProductOptionValueConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int ValueMaxLength = 125;
}