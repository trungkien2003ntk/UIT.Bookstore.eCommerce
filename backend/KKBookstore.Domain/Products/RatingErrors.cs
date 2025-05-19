using KKBookstore.Models;

namespace KKBookstore.Products;

public static class RatingErrors
{
    public static readonly Error CommentTooLong = Error.Validation(
        "Rating.CommentTooLong",
        $"Comment must be at most {RatingConsts.CommentMaxLength} characters long."
    );

    public static readonly Error ReportReasonRequired = Error.Validation(
        "Rating.ReportReasonRequired",
        "Report reason is required."
    );

    public static readonly Error AlreadyReported = Error.Validation(
        "Rating.AlreadyReported",
        "You have already reported this rating."
    );
}
