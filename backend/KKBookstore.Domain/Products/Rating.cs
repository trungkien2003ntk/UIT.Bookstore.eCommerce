using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Products;

public class Rating : BaseAuditedEntity
{
    public Rating()
    {

    }
    private Rating(
        string comment,
        int ratingValue,
        int userId,
        int productVariantId
    ) : base()
    {
        Comment = comment;
        RatingValue = ratingValue;
        CustomerId = userId;
        ProductVariantId = productVariantId;
    }

    public string? Comment { get; set; }
    public int RatingValue { get; set; }
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
    public int ReportedCount { get; set; }
    public string? Response { get; set; }
    public RatingStatus Status { get; set; }

    // navigation property
    public Customer Customer { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public ICollection<RatingLike> Likes { get; set; }

    public static Result<Rating> Create(
        string comment,
        int ratingValue,
        int userId,
        int productVariantId
    )
    {
        return new Rating(comment, ratingValue, userId, productVariantId);
    }
}
