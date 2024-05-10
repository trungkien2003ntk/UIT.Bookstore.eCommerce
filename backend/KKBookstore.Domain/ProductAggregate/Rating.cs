using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class Rating : BaseEntity, ITrackable
{
    public int RatingId { get; set; }
    public string Comment { get; set; }
    public int RatingValue { get; set; }
    public int UserId { get; set; }
    public int SkuId { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User User { get; set; }
    public Sku Sku { get; set; }
    public User LastEditedByUser { get; set; }
}
