namespace KKBookstore.Domain.Shared.Orders;

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
