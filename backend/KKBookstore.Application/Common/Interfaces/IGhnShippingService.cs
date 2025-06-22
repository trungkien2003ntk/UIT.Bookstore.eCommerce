using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;

namespace KKBookstore.Common.Interfaces;

/// <summary>
/// Service interface for integrating with Giao Hang Nhanh (GHN) shipping API.
/// Provides methods for all major GHN operations including order management, 
/// delivery time calculations, and store management.
/// </summary>
public interface IGhnShippingService
{
    /// <summary>
    /// Creates a new shipping order with GHN.
    /// Use this when a customer places an order and you need to ship it via GHN.
    /// </summary>
    /// <param name="request">The order creation request with customer details, items, and shipping info</param>
    /// <returns>The created order details including tracking code and order_code</returns>
    Task<CreateGhnOrderResult> CreateOrderAsync(CreateGhnOrderRequest request);

    /// <summary>
    /// Retrieves detailed information about an existing GHN order.
    /// Use this to track order status, delivery progress, and get updated information.
    /// </summary>
    /// <param name="orderCode">The GHN order code returned when creating the order</param>
    /// <returns>Complete order information including status, fees, and delivery details</returns>
    Task<GhnOrderInfoResult> GetOrderInfoAsync(string orderCode);

    /// <summary>
    /// Cancels an existing GHN order that hasn't been picked up yet.
    /// Use this when a customer cancels their order before shipping or if there are issues.
    /// </summary>
    /// <param name="orderCode">The GHN order code to cancel</param>
    /// <returns>Result indicating success or failure of the cancellation</returns>
    Task<GhnOrderOperationResult> CancelOrderAsync(string orderCode);

    /// <summary>
    /// Creates a return/exchange order for items that need to be sent back.
    /// Use this when customers want to return products or exchange them.
    /// </summary>
    /// <param name="request">The return order request with original order details and return items</param>
    /// <returns>The created return order details including new tracking information</returns>
    Task<CreateGhnOrderResult> CreateReturnOrderAsync(CreateGhnOrderRequest request);

    /// <summary>
    /// Updates the Cash on Delivery (COD) amount for an existing order.
    /// Use this when the order total changes due to promotions, discounts, or item modifications.
    /// </summary>
    /// <param name="request">The COD update request with new amount and order details</param>
    /// <returns>Result indicating success or failure of the COD update</returns>
    Task<GhnOrderOperationResult> UpdateCodAsync(UpdateGhnCodRequest request);

    /// <summary>
    /// Calculates the expected delivery time for shipping between two locations.
    /// Use this during checkout to show customers estimated delivery dates.
    /// </summary>
    /// <param name="request">The delivery time calculation request with origin and destination details</param>
    /// <returns>Expected delivery date and time information</returns>
    Task<GhnDeliveryTimeResult> CalculateExpectedDeliveryTimeAsync(CalculateGhnDeliveryTimeRequest request);

    /// <summary>
    /// Retrieves available pickup time slots for a specific date and location.
    /// Use this to let customers choose when GHN should pick up their orders.
    /// </summary>
    /// <returns>List of available pickup shifts with time slots</returns>
    Task<GhnPickShiftResult> GetPickShiftAsync();

    /// <summary>
    /// Creates a new store/pickup location in the GHN system.
    /// Use this when setting up new business locations or warehouses for order fulfillment.
    /// </summary>
    /// <param name="request">The store creation request with address and contact details</param>
    /// <returns>The created store information including store ID for future operations</returns>
    Task<CreateGhnStoreResult> CreateStoreAsync(CreateGhnStoreRequest request);

    /// <summary>
    /// Processes webhook status updates received from GHN delivery service.
    /// Use this to handle real-time order status changes and update your system accordingly.    /// </summary>
    /// <param name="orderCode">The GHN order code that had a status update</param>
    /// <param name="ghnStatus">The new status from GHN</param>
    /// <param name="reason">Optional reason for the status change</param>
    /// <returns>Result indicating success or failure of processing the status update</returns>
    Task<Result> ProcessOrderStatusUpdateAsync(string orderCode, string ghnStatus, string? reason = null);

    /// <summary>
    /// Processes an order status update from GHN webhook system with user context for manual triggers.
    /// Updates the local order status based on GHN delivery status changes and records who triggered the update.
    /// </summary>
    /// <param name="orderCode">The GHN order code that had a status update</param>
    /// <param name="ghnStatus">The new status from GHN</param>
    /// <param name="reason">Optional reason for the status change</param>
    /// <param name="triggeredByUserId">ID of the user who manually triggered the update (null for automatic webhooks)</param>
    /// <returns>Result indicating success or failure of processing the status update</returns>
    Task<Result> ProcessOrderStatusUpdateAsync(string orderCode, string ghnStatus, string? reason = null, int? triggeredByUserId = null);
}