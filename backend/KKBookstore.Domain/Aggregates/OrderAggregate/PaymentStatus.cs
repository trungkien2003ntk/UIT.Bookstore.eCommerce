namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}
