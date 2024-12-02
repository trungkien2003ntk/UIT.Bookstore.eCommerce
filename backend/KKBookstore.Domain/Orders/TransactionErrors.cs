using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Orders;

public static class TransactionErrors
{
    public static readonly Error Unknown = Error.Failure("Transaction.Unknown", "An unknown error occurred");
    public static readonly Error BadRequest = Error.Validation("Transaction.BadRequest", "Bad request");
    public static readonly Error InvalidCredentials = Error.Unauthorized("Transaction.InvalidCredentials", "Invalid credentials");
    public static readonly Error PaymentError = Error.Failure("Transaction.PaymentError", "Payment error");
    public static readonly Error Forbidden = Error.Unauthorized("Transaction.Forbidden", "Forbidden");
    public static readonly Error ServiceUnavailable = Error.Unauthorized("Transaction.ServiceUnavailable", "Service is unavailable at this moment");
    public static readonly Error FailedToCommitTransaction = Error.Failure("Transaction.FailedToCommitTransaction", "Failed to commit transaction");
}
