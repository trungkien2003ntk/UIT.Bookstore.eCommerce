using KKBookstore.Customers;
using KKBookstore.Models;

namespace KKBookstore.Products;

public class RatingLike : BaseAuditedEntity
{
    public int RatingId { get; set; }
    public int CustomerId { get; set; }
    public bool Liked { get; set; }
    public DateTimeOffset LikedTime { get; set; }
    public Rating Rating { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
