namespace KKBookstore.Domain.Shared.Products;

public static class ProductVariantConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 256;
    public const int CommentMaxLength = 1024;
    public const int SkuMaxLength = 64;
    public const int BarcodeMaxLength = 64;
    public const int ImageUrlMaxLength = 1024;
    public const int NoteMaxLength = 1024;
}