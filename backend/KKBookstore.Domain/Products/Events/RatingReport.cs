using KKBookstore.Models;

namespace KKBookstore.Products.Events;

public class RatingReport : BaseAuditedEntity
{
    public RatingReport(
        int ratingId,
        int customerId,
        string reason
    ) : base()
    {
        RatingId = ratingId;
        CustomerId = customerId;
        Reason = reason;
    }

    protected RatingReport() : base()
    {
    }

    public int RatingId { get; set; }
    public int CustomerId { get; set; }
    public string Reason { get; set; } = null!;
    // navigation property
    public Rating Rating { get; set; } = null!;
}
