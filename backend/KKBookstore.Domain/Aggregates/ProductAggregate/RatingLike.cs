using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.ProductAggregate;

public class RatingLike : BaseEntity
{
    public int RatingId { get; set; }
    public int UserId { get; set; }
    public bool Liked { get; set; }
    public DateTimeOffset LikedWhen { get; set; }
    public Rating Rating { get; set; }
    public User User { get; set; }
}
