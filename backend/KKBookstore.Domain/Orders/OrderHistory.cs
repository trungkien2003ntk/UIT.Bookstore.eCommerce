using KKBookstore.Models;
using KKBookstore.Users;

namespace KKBookstore.Orders;

public class OrderHistory : BaseAuditedEntity
{
    public OrderHistory()
    {
        Timestamp = DateTimeOffset.Now;
        Action = string.Empty; // Initialize to empty string
    }

    private OrderHistory(
        int orderId,
        OrderStatus fromStatus,
        OrderStatus toStatus,
        string action,
        string? notes = null,
        int? triggeredByUserId = null,
        string? externalReference = null
    ) : base()
    {
        OrderId = orderId;
        FromStatus = fromStatus;
        ToStatus = toStatus;
        Action = action;
        Notes = notes;
        TriggeredByUserId = triggeredByUserId;
        ExternalReference = externalReference;
        Timestamp = DateTimeOffset.Now;
    }

    public int OrderId { get; set; }
    public OrderStatus FromStatus { get; set; }
    public OrderStatus ToStatus { get; set; }
    public string Action { get; set; }
    public string? Notes { get; set; }
    public int? TriggeredByUserId { get; set; }
    public string? ExternalReference { get; set; } // For GHN order codes, webhook references, etc.
    public DateTimeOffset Timestamp { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!; // Required navigation property
    public User? TriggeredByUser { get; set; }

    // Factory method
    public static Result<OrderHistory> Create(
        int orderId,
        OrderStatus fromStatus,
        OrderStatus toStatus,
        string action,
        string? notes = null,
        int? triggeredByUserId = null,
        string? externalReference = null)
    {
        if (string.IsNullOrWhiteSpace(action))
        {
            return Result.Failure<OrderHistory>(Error.Validation("OrderHistory.Action", "Action is required"));
        }

        return new OrderHistory(
            orderId,
            fromStatus,
            toStatus,
            action,
            notes,
            triggeredByUserId,
            externalReference
        );
    }
}
