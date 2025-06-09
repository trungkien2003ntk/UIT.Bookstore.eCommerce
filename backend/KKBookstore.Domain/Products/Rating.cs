using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Products.Events;

namespace KKBookstore.Products;

public class Rating : BaseAuditedEntity
{
    public Rating()
    {

    }
    private Rating(
        string comment,
        int ratingValue,
        int customerId,
        ProductVariant variant,
        List<string> imageUrls
    ) : base()
    {
        Comment = comment;
        RatingValue = ratingValue;
        CustomerId = customerId;
        ProductVariantId = variant.Id;
        ProductId = variant.ProductId;
        Status = RatingStatus.Posted;
        Images = [.. imageUrls.Select(imageUrls => new RatingImage(imageUrls, 0))];
    }

    public string? Comment { get; set; }
    public int RatingValue { get; set; }
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int ProductId { get; set; }
    public string? Response { get; set; }
    public RatingStatus Status { get; set; }

    public int ReportsCount { get; set; }

    // navigation property
    public Customer Customer { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public ICollection<RatingLike> Likes { get; set; }
    public ICollection<RatingImage>? Images { get; set; }
    public ICollection<RatingReport>? Reports { get; set; }

    public static Result<Rating> Create(
        string comment,
        int ratingValue,
        int customerId,
        ProductVariant variant,
        List<string> imageUrls
    )
    {
        // validate comment

        if (string.IsNullOrWhiteSpace(comment) || comment.Length > RatingConsts.CommentMaxLength)
        {
            return Result.Failure<Rating>(RatingErrors.CommentTooLong);
        }

        return new Rating(comment, ratingValue, customerId, variant, imageUrls);
    }

    public Result ReportedBy(int customerId, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(RatingErrors.ReportReasonRequired);

        var report = new RatingReport(Id, customerId, reason);

        Reports ??= [];
        if (Reports.Any(r => r.CustomerId == customerId))
        {
            return Result.Failure(RatingErrors.AlreadyReported);
        }

        Reports.Add(report);
        ReportsCount++;

        if (ReportsCount >= RatingConsts.ReportThreshold && Status == RatingStatus.Posted)
        {
            Status = RatingStatus.PendingReview;
        }

        return Result.Success();
    }
}
