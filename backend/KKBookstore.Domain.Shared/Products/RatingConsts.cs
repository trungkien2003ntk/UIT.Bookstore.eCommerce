namespace KKBookstore.Domain.Shared.Products;

public static class RatingConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int CommentMaxLength = 2048;
    public const int ResponseMaxLength = 2048;
    public const int RateMaxValue = 5;
    public const int RateMinValue = 1;
}