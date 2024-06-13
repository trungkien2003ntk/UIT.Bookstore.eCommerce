namespace KKBookstore.Application.Extensions;

public static class PriceExtension
{
    public static string ToFormattedVietnamesePrice(this decimal price)
    {
        return $"{price:#,##0}₫";
    }
}
