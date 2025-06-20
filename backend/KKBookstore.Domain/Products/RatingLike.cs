using KKBookstore.Customers;
using KKBookstore.Models;

namespace KKBookstore.Products;

public class RatingLike : BaseFullAuditedEntity
{
    public RatingLike(
        int ratingId,
        int customerId,
        bool liked,
        DateTimeOffset likedTime
    ) : base()
    {
        RatingId = ratingId;
        CustomerId = customerId;
        Liked = liked;
        LikedTime = likedTime;
    }

    protected RatingLike() : base()
    {

    }
    public int RatingId { get; set; }
    public int CustomerId { get; set; }
    public bool Liked { get; set; }
    public DateTimeOffset LikedTime { get; set; }
    public Rating Rating { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
