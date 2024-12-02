namespace KKBookstore.Domain.Shared.ProductTypes;

public static class ProductTypeAttributeValueConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int ValueMaxLength = 1024;
}