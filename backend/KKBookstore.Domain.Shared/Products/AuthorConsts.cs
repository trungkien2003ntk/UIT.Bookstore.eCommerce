namespace KKBookstore.Domain.Shared.Products;

public static class AuthorConsts
{
    private const string DefaultSorting = "Name asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 256;
    public const int DescriptionMaxLength = 1024;
}