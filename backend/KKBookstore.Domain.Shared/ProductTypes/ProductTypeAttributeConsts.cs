namespace KKBookstore.Domain.Shared.ProductTypes;

public static class ProductTypeAttributeConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 125;
}