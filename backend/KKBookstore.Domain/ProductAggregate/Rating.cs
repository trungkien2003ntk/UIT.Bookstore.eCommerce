using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class Rating : BaseAuditableEntity
{
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

    public string Comment { get; set; }
    public int RatingValue { get; set; }
    public int UserId { get; set; }
    public int SkuId { get; set; }

    // navigation property
    public User User { get; set; }
    public Sku Sku { get; set; }

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
