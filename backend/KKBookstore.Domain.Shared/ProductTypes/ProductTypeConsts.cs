namespace KKBookstore.Domain.Shared.ProductTypes;

public static class ProductTypeConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int DisplayNameMaxLength = 250;
    public const int ProductTypeCodeMaxLength = 50;
    public const int DescriptionMaxLength = 1024;

}