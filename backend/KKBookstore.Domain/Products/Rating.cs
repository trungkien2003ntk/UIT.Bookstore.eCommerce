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

    // AI Moderation properties
    public int? AiModerationScore { get; set; } // Badness score from AI (1-100)
    public string? AiModerationCategory { get; set; } // Category of violation
    public string? AiModerationExplanation { get; set; } // AI explanation
    public DateTimeOffset? AiModerationDate { get; set; } // When AI evaluation occurred
    public bool IsAiModerated { get; set; } // Whether this rating has been AI moderated    // navigation property
    public Customer Customer { get; set; } = null!;
    public ProductVariant ProductVariant { get; set; } = null!;
    public ICollection<RatingLike> Likes { get; set; } = new List<RatingLike>();
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
    public Result ReportedBy(int customerId, string reason, string? detailedReason = null)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(RatingErrors.ReportReasonRequired);

        var report = new RatingReport(Id, customerId, reason, detailedReason);

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

    public void SetAiModerationResult(int score, string category, string explanation)
    {
        AiModerationScore = score;
        AiModerationCategory = category;
        AiModerationExplanation = explanation;
        AiModerationDate = DateTimeOffset.UtcNow;
        IsAiModerated = true;
    }

    public bool ShouldBeAutoHidden(int autoHideThreshold)
    {
        return IsAiModerated && AiModerationScore.HasValue && AiModerationScore.Value >= autoHideThreshold;
    }
}
