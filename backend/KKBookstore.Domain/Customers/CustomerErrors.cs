using KKBookstore.Models;

namespace KKBookstore.Customers;

public static class CustomerErrors
{
    public static readonly Error NotFound = Error.NotFound("Customer.NotFound", "Customer was not found.");
    public static readonly Error CreateFailed = Error.Failure("Customer.CreateFailed", "Customer creation failed.");
    public static readonly Error AlreadyExists = Error.Conflict("Customer.AlreadyExists", "Customer already exists.");
    public static readonly Error UpdateFailed = Error.Failure("Customer.UpdateFailed", "Customer update failed."); public static readonly Error BlockFailed = Error.Failure("Customer.BlockFailed", "Customer blocking failed.");
    public static readonly Error AlreadyBlocked = Error.Conflict("Customer.AlreadyBlocked", "Customer is already blocked.");
    public static readonly Error UnblockFailed = Error.Failure("Customer.UnblockFailed", "Customer unblocking failed.");
    public static readonly Error AlreadyActive = Error.Conflict("Customer.AlreadyActive", "Customer is already active.");
    public static readonly Error CustomerTypeNotFound = Error.NotFound("Customer.CustomerTypeNotFound", "Customer type was not found.");
}
