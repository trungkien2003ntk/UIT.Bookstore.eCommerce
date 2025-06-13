using KKBookstore.Models;

namespace KKBookstore.Shipping;

/// <summary>
/// Error definitions for GHN shipping service operations.
/// Follows the domain error pattern used throughout the application.
/// </summary>
public static class GhnShippingErrors
{
    public static readonly Error OrderNotFound = Error.NotFound(
        "GhnShipping.OrderNotFound",
        "Order not found for the specified GHN order code");

    public static readonly Error InvalidStatus = Error.Validation(
        "GhnShipping.InvalidStatus",
        "Invalid GHN status provided");

    public static readonly Error ProcessingFailed = Error.Failure(
        "GhnShipping.ProcessingFailed",
        "Failed to process GHN webhook update");

    public static readonly Error DatabaseUpdateFailed = Error.Failure(
        "GhnShipping.DatabaseUpdateFailed",
        "Failed to update order status in database");

    public static readonly Error CodUpdateFailed = Error.Failure(
        "GhnShipping.CodUpdateFailed",
        "Failed to update COD amount");

    public static readonly Error ConfigurationInvalid = Error.Validation(
        "GhnShipping.ConfigurationInvalid",
        "GHN shipping configuration is invalid or missing");

    public static Error WebhookProcessingFailed => Error.Failure(
        "GhnShipping.WebhookProcessingFailed",
        $"GHN webhook processing failed");

    public static Error OrderCodeNotFound(string orderCode) => Error.NotFound(
        "GhnShipping.OrderCodeNotFound",
        $"Order with GHN code '{orderCode}' was not found");

    public static Error ProcessingError(string message) => Error.Failure(
        "GhnShipping.ProcessingError",
        $"Error processing GHN webhook: {message}");
}
