namespace KKBookstore.Products;

public static class ProductOptionConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 125;
}