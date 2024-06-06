using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public static class OrderErrors
{
    // list all possible order error using the KKBookstore.Domain.Common.Error record
    public static readonly Error OrderNotFound = Error.NotFound("Order.Error.OrderNotFound", "Order was not found");
    public static readonly Error OrderAlreadyCancelled = Error.Conflict("Order.Error.OrderAlreadyCancelled", "Order was already cancelled");
    public static readonly Error OrderAlreadyDelivered = Error.Conflict("Order.Error.OrderAlreadyDelivered", "Order was already delivered");
    public static readonly Error OrderAlreadyReceived = Error.Conflict("Order.Error.OrderAlreadyReceived", "Order was already received");
    public static readonly Error OrderAlreadyShipped = Error.Conflict("Order.Error.OrderAlreadyShipped", "Order was already shipped");
    public static readonly Error OrderAlreadyPacked = Error.Conflict("Order.Error.OrderAlreadyPacked", "Order was already packed");
    public static readonly Error OrderAlreadyPicked = Error.Conflict("Order.Error.OrderAlreadyPicked", "Order was already picked");
    public static readonly Error OrderAlreadyPending = Error.Conflict("Order.Error.OrderAlreadyPending", "Order was already pending");
    public static readonly Error OrderAlreadyShipping = Error.Conflict("Order.Error.OrderAlreadyShipping", "Order was already shipping");

    public static readonly Error OrderItemNotFound = Error.NotFound("Order.Error.OrderItemNotFound", "Order item was not found");
    public static readonly Error OrderItemAlreadyCancelled = Error.Conflict("Order.Error.OrderItemAlreadyCancelled", "Order item was already cancelled");
    public static readonly Error OrderItemAlreadyDelivered = Error.Conflict("Order.Error.OrderItemAlreadyDelivered", "Order item was already delivered");
    public static readonly Error OrderItemAlreadyReceived = Error.Conflict("Order.Error.OrderItemAlreadyReceived", "Order item was already received");
    public static readonly Error OrderItemAlreadyShipped = Error.Conflict("Order.Error.OrderItemAlreadyShipped", "Order item was already shipped");
    public static readonly Error OrderItemAlreadyPacked = Error.Conflict("Order.Error.OrderItemAlreadyPacked", "Order item was already packed");
    public static readonly Error OrderItemAlreadyPicking = Error.Conflict("Order.Error.OrderItemAlreadyPicking", "Order item was already picking");
    public static readonly Error OrderItemAlreadyPicked = Error.Conflict("Order.Error.OrderItemAlreadyPicked", "Order item was already picked");
    public static readonly Error OrderItemAlreadyProcessing = Error.Conflict("Order.Error.OrderItemAlreadyProcessing", "Order item was already processing");
    public static readonly Error OrderItemAlreadyPending = Error.Conflict("Order.Error.OrderItemAlreadyPending", "Order item was already pending");
    public static readonly Error OrderItemAlreadyShipping = Error.Conflict("Order.Error.OrderItemAlreadyShipping", "Order item was already shipping");
    public static readonly Error OrderItemAlreadyAdded = Error.Conflict("Order.Error.OrderItemAlreadyAdded", "Order item was already added");
    public static readonly Error OrderItemAlreadyRemoved = Error.Conflict("Order.Error.OrderItemAlreadyRemoved", "Order item was already removed");
    public static readonly Error OrderItemQuantityExceeded = Error.BusinessRuleViolation("Order.Error.OrderItemQuantityExceeded", "Order item quantity exceeded");
    public static readonly Error OrderItemQuantityInvalid = Error.Validation("Order.Error.OrderItemQuantityInvalid", "Order item quantity is invalid");
    public static readonly Error OrderItemQuantityNotRemoved = Error.BusinessRuleViolation("Order.Error.OrderItemQuantityNotRemoved", "Order item quantity was not removed");

    public static readonly Error OrderPaymentFailed = Error.Failure("Order.Error.OrderPaymentFailed", "Order payment failed");
    public static readonly Error OrderAuthorizationFailed = Error.Unauthorized("Order.Error.OrderAuthorizationFailed", "Order authorization failed");
    public static readonly Error OrderInternalError = Error.Failure("Order.Error.OrderInternalError", "Order internal error");

    public static readonly Error MissingReturnUrl = Error.Validation("Order.Error.MissingReturnUrl", "Missing return url");
    public static readonly Error InsufficientStock = Error.BusinessRuleViolation("Order.Error.InsufficientStock", "Insufficient stock");
    public static readonly Error OrderCreationFailed = Error.Failure("Order.Error.OrderCreationFailed", "Order creation failed");
}
