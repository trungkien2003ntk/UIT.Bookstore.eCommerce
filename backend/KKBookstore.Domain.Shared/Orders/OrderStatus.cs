namespace KKBookstore.Orders;

public enum OrderStatus
{
    // list all possible commmon order status
    Pending,
    Processing,
    WaitForConfirmPackageBranch, // When order needs admin selection for packaging branch
    Packaging,
    Shipped,
    Delivered,
    Received,
    Cancelled,
    Refunded,
}
