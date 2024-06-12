namespace KKBookstore.Application.Extensions;

public static class DateTimeOffsetExtension
{
    public static string ToGmtPlus7FormattedString(this DateTimeOffset dateTimeOffset)
    {
        // Define GMT+7 timezone
        TimeZoneInfo gmtPlus7TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        // Convert to GMT+7
        DateTimeOffset gmtPlus7DateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, gmtPlus7TimeZone);

        // Format and return the string
        return gmtPlus7DateTime.ToString("dd/MM/yyyy, h:mm tt");
    }
}
