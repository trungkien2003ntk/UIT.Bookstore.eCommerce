namespace KKBookstore.Domain.OrderAggregate;

public enum OrderStatus
{
    // list all possible commmon order status
    Pending,
    Processing,
    Picking,
    Picked,
    Packing,
    Packed,
    Shipping,
    Shipped,
    Delivered,
    Received,
    Cancelled
}
