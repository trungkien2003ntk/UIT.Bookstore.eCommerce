using KKBookstore.Models;

namespace KKBookstore.Products.Events;

public class RatingReport : BaseAuditedEntity
{
    public RatingReport(
        int ratingId,
        int customerId,
        string reason,
        string? detailedReason = null
    ) : base()
    {
        RatingId = ratingId;
        CustomerId = customerId;
        Reason = reason;
        DetailedReason = detailedReason;
    }    protected RatingReport() : base()
    {
    }

    public int RatingId { get; set; }
    public int CustomerId { get; set; }
    public string Reason { get; set; } = null!;
    public string? DetailedReason { get; set; }
    
    // navigation property
    public Rating Rating { get; set; } = null!;
}
