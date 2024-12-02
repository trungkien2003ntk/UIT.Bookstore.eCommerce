using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public class RatingLike : BaseAuditedEntity
{
    public int RatingId { get; set; }
    public int CustomerId { get; set; }
    public bool Liked { get; set; }
    public DateTimeOffset LikedTime { get; set; }
    public Rating Rating { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
