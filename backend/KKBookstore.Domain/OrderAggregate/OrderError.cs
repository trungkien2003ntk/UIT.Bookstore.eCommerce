using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.OrderAggregate;

public static class OrderError
{
    // list all possible order error using the KKBookstore.Domain.Common.Error record
    public static readonly Error OrderNotFound = new("Order.Error.OrderNotFound", "Order was not found");
    public static readonly Error OrderAlreadyCancelled = new("Order.Error.OrderAlreadyCancelled", "Order was already cancelled");
    public static readonly Error OrderAlreadyDelivered = new("Order.Error.OrderAlreadyDelivered", "Order was already delivered");
    public static readonly Error OrderAlreadyReceived = new("Order.Error.OrderAlreadyReceived", "Order was already received");
    public static readonly Error OrderAlreadyShipped = new("Order.Error.OrderAlreadyShipped", "Order was already shipped");
    public static readonly Error OrderAlreadyPacked = new("Order.Error.OrderAlreadyPacked", "Order was already packed");
    public static readonly Error OrderAlreadyPicked = new("Order.Error.OrderAlreadyPicked", "Order was already picked");
    public static readonly Error OrderAlreadyPending = new("Order.Error.OrderAlreadyPending", "Order was already pending");
    public static readonly Error OrderAlreadyShipping = new("Order.Error.OrderAlreadyShipping", "Order was already shipping");

    public static readonly Error OrderItemNotFound = new("Order.Error.OrderItemNotFound", "Order item was not found");
    public static readonly Error OrderItemAlreadyCancelled = new("Order.Error.OrderItemAlreadyCancelled", "Order item was already cancelled");
    public static readonly Error OrderItemAlreadyDelivered = new("Order.Error.OrderItemAlreadyDelivered", "Order item was already delivered");
    public static readonly Error OrderItemAlreadyReceived = new("Order.Error.OrderItemAlreadyReceived", "Order item was already received");
    public static readonly Error OrderItemAlreadyShipped = new("Order.Error.OrderItemAlreadyShipped", "Order item was already shipped");
    public static readonly Error OrderItemAlreadyPacked = new("Order.Error.OrderItemAlreadyPacked", "Order item was already packed");
    public static readonly Error OrderItemAlreadyPicking = new("Order.Error.OrderItemAlreadyPicking", "Order item was already picking");
    public static readonly Error OrderItemAlreadyPicked = new("Order.Error.OrderItemAlreadyPicked", "Order item was already picked");
    public static readonly Error OrderItemAlreadyProcessing = new("Order.Error.OrderItemAlreadyProcessing", "Order item was already processing");
    public static readonly Error OrderItemAlreadyPending = new("Order.Error.OrderItemAlreadyPending", "Order item was already pending");
    public static readonly Error OrderItemAlreadyShipping = new("Order.Error.OrderItemAlreadyShipping", "Order item was already shipping");
    public static readonly Error OrderItemAlreadyAdded = new("Order.Error.OrderItemAlreadyAdded", "Order item was already added");
    public static readonly Error OrderItemAlreadyRemoved = new("Order.Error.OrderItemAlreadyRemoved", "Order item was already removed");
    public static readonly Error OrderItemQuantityExceeded = new("Order.Error.OrderItemQuantityExceeded", "Order item quantity exceeded");
    public static readonly Error OrderItemQuantityInvalid = new("Order.Error.OrderItemQuantityInvalid", "Order item quantity is invalid");
    public static readonly Error OrderItemQuantityNotRemoved = new("Order.Error.OrderItemQuantityNotRemoved", "Order item quantity was not removed");

    public static readonly Error OrderPaymentFailed = new("Order.Error.OrderPaymentFailed", "Order payment failed");
    public static readonly Error OrderAuthorizationFailed = new("Order.Error.OrderAuthorizationFailed", "Order authorization failed");
    public static readonly Error OrderInternalError = new("Order.Error.OrderInternalError", "Order internal error");
}
