namespace KKBookstore.Products;

public static class RatingConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int ReportThreshold = 3;

    public const int CommentMaxLength = 2048;
    public const int ResponseMaxLength = 2048;
    public const int RateMaxValue = 5;
    public const int RateMinValue = 1;
}