using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.Users;

namespace KKBookstore.Common.Interfaces;

/// <summary>
/// Service for intelligent branch selection and inventory allocation based on customer location
/// </summary>
public interface IBranchSelectionService
{
    /// <summary>
    /// Allocates inventory from the nearest branches for an order using FIFO strategy
    /// </summary>
    /// <param name="order">The order to process</param>
    /// <param name="customerAddress">Customer's shipping address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of order fulfillments with allocated inventory</returns>
    Task<Result<List<OrderFulfillment>>> AllocateInventoryFromNearestBranchesAsync(
        Order order,
        Address customerAddress,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if order fulfillment requires admin confirmation (multiple branches involved)
    /// </summary>
    /// <param name="orderFulfillments">List of order fulfillments</param>
    /// <returns>True if admin confirmation is needed</returns>
    bool RequiresAdminConfirmation(List<OrderFulfillment> orderFulfillments);

    /// <summary>
    /// Gets all branches ordered by distance from customer address
    /// </summary>
    /// <param name="customerAddress">Customer's address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Branches with distance information</returns>
    Task<Result<List<BranchDistanceInfo>>> GetBranchesByDistanceAsync(
        Address customerAddress,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Contains branch information with distance from customer
/// </summary>
public class BranchDistanceInfo
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public double DistanceKm { get; set; }
    public GeoCoordResult Coordinates { get; set; } = new();
    public string FormattedAddress { get; set; } = string.Empty;
}
