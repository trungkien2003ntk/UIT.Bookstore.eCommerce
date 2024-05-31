namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public enum OrderStatus
{
    // list all possible commmon order status
    Pending,
    Processing,
    Shipped,
    Delivered,
    Received,
    Cancelled,
    Refunded,
}
