using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class Rating : BaseAuditableEntity
{
    public Rating()
    {

    }
    private Rating(
        string comment,
        int ratingValue,
        int userId,
        int skuId
    ) : base()
    {
        Comment = comment;
        RatingValue = ratingValue;
        UserId = userId;
        SkuId = skuId;
    }

    public string? Comment { get; set; }
    public int RatingValue { get; set; }
    public int UserId { get; set; }
    public int SkuId { get; set; }
    public int ReportedCount { get; set; }
    public string? Response { get; set; }
    public RatingStatus Status { get; set; }

    // navigation property
    public User User { get; set; }
    public Sku Sku { get; set; }
    public ICollection<RatingLike> Likes { get; set; }

    public static Result<Rating> Create(
        string comment,
        int ratingValue,
        int userId,
        int skuId
    )
    {
        return new Rating(comment, ratingValue, userId, skuId);
    }
}
