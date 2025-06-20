using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Services;

/// <summary>
/// Intelligent branch selection service that allocates inventory from nearest branches using FIFO strategy
/// </summary>
public class BranchSelectionService : IBranchSelectionService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IGeoCoordService _geoCoordService;
    private readonly ILogger<BranchSelectionService> _logger;

    public BranchSelectionService(
        IApplicationDbContext dbContext,
        IGeoCoordService geoCoordService,
        ILogger<BranchSelectionService> logger)
    {
        _dbContext = dbContext;
        _geoCoordService = geoCoordService;
        _logger = logger;
    }

    public async Task<Result<List<OrderFulfillment>>> AllocateInventoryFromNearestBranchesAsync(
        Order order,
        Address customerAddress,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting inventory allocation for order {OrderId} from nearest branches", order.Id);

            // Get customer coordinates
            var customerCoords = await _geoCoordService.GetCoordinatesAsync(customerAddress, cancellationToken);
            if (!customerCoords.Success)
            {
                _logger.LogError("Failed to geocode customer address for order {OrderId}: {Error}",
                    order.Id, customerCoords.ErrorMessage);
                return Result.Failure<List<OrderFulfillment>>(
                    Error.Failure("BranchSelection.GeocodingFailed", customerCoords.ErrorMessage ?? "Failed to geocode customer address"));
            }

            // Get all branches with their distances
            var branchesResult = await GetBranchesByDistanceAsync(customerAddress, cancellationToken);
            if (branchesResult.IsFailure)
            {
                return Result.Failure<List<OrderFulfillment>>(branchesResult.Error);
            }

            var branchesByDistance = branchesResult.Value;
            var orderFulfillments = new List<OrderFulfillment>();
            var unallocatedItems = new Dictionary<int, int>(); // ProductVariantId -> Remaining Quantity

            // Initialize unallocated items from order lines
            foreach (var orderLine in order.OrderLines)
            {
                unallocatedItems[orderLine.ProductVariantId!.Value] = orderLine.Quantity;
            }

            // Try to allocate inventory from each branch, starting with the nearest
            foreach (var branchInfo in branchesByDistance)
            {
                if (!unallocatedItems.Any(x => x.Value > 0))
                    break; // All items allocated

                var fulfillment = await TryAllocateFromBranch(
                    order, branchInfo, unallocatedItems, cancellationToken);

                if (fulfillment != null)
                {
                    orderFulfillments.Add(fulfillment);
                }
            }

            // Check if all items were allocated
            var remainingItems = unallocatedItems.Where(x => x.Value > 0).ToList();
            if (remainingItems.Any())
            {
                _logger.LogError("Could not allocate all items for order {OrderId}. Remaining: {RemainingItems}",
                    order.Id, string.Join(", ", remainingItems.Select(x => $"Variant {x.Key}: {x.Value}")));

                return Result.Failure<List<OrderFulfillment>>(
                    Error.Failure("BranchSelection.InsufficientInventory",
                        $"Insufficient inventory for variants: {string.Join(", ", remainingItems.Select(x => x.Key))}"));
            }

            _logger.LogInformation("Successfully allocated inventory for order {OrderId} across {BranchCount} branches",
                order.Id, orderFulfillments.Count);

            return Result.Success(orderFulfillments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error allocating inventory for order {OrderId}", order.Id);
            return Result.Failure<List<OrderFulfillment>>(
                Error.Failure("BranchSelection.AllocationError", ex.Message));
        }
    }

    public bool RequiresAdminConfirmation(List<OrderFulfillment> orderFulfillments)
    {
        return orderFulfillments.Count > 1;
    }

    public async Task<Result<List<BranchDistanceInfo>>> GetBranchesByDistanceAsync(
        Address customerAddress,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting branches by distance from customer address");

            // Get customer coordinates
            var customerCoords = await _geoCoordService.GetCoordinatesAsync(customerAddress, cancellationToken);
            if (!customerCoords.Success)
            {
                return Result.Failure<List<BranchDistanceInfo>>(
                    Error.Failure("BranchSelection.GeocodingFailed", customerCoords.ErrorMessage ?? "Failed to geocode customer address"));
            }

            // Get all active branches with their addresses
            var branches = await _dbContext.Branches
                .Include(b => b.Address)
                .Where(b => !b.IsDeleted)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var branchDistances = new List<BranchDistanceInfo>();

            // Calculate distance to each branch
            foreach (var branch in branches)
            {
                var branchCoords = await _geoCoordService.GetCoordinatesAsync(branch.Address, cancellationToken);
                if (branchCoords.Success)
                {
                    var distance = customerCoords.DistanceTo(branchCoords);
                    branchDistances.Add(new BranchDistanceInfo
                    {
                        BranchId = branch.Id,
                        BranchName = branch.Name,
                        DistanceKm = distance,
                        Coordinates = branchCoords,
                        FormattedAddress = branchCoords.FormattedAddress ?? branch.Address.ToString()
                    });
                }
                else
                {
                    _logger.LogWarning("Failed to geocode branch {BranchId} address: {Error}",
                        branch.Id, branchCoords.ErrorMessage);
                }
            }

            // Sort by distance (nearest first)
            var sortedBranches = branchDistances.OrderBy(b => b.DistanceKm).ToList();

            _logger.LogInformation("Found {BranchCount} branches within range. Nearest: {NearestBranch} ({Distance:F2}km)",
                sortedBranches.Count,
                sortedBranches.FirstOrDefault()?.BranchName,
                sortedBranches.FirstOrDefault()?.DistanceKm);

            return Result.Success(sortedBranches);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting branches by distance");
            return Result.Failure<List<BranchDistanceInfo>>(
                Error.Failure("BranchSelection.DistanceCalculationError", ex.Message));
        }
    }

    private async Task<OrderFulfillment?> TryAllocateFromBranch(
        Order order,
        BranchDistanceInfo branchInfo,
        Dictionary<int, int> unallocatedItems,
        CancellationToken cancellationToken)
    {
        var allocations = new List<OrderLineAllocation>();
        var itemsToAllocate = unallocatedItems.Where(x => x.Value > 0).ToList();

        foreach (var (productVariantId, neededQuantity) in itemsToAllocate)
        {
            // Get available inventory for this variant at this branch (FIFO)
            var availableInventory = await _dbContext.Inventories
                .Where(i => i.ProductVariantId == productVariantId &&
                           i.WarehouseId == branchInfo.BranchId &&
                           i.IsActive &&
                           i.StockQuantity > 0)
                .OrderBy(i => i.OriginalCreatedDate) // FIFO - oldest first
                .ToListAsync(cancellationToken);

            var totalAvailable = availableInventory.Sum(i => i.StockQuantity);
            if (totalAvailable == 0)
                continue; // No inventory available at this branch

            // Allocate as much as possible from this branch
            var quantityToAllocate = Math.Min(neededQuantity, totalAvailable);
            var remainingToAllocate = quantityToAllocate;

            // Find the corresponding order line for unit price
            var orderLine = order.OrderLines.First(ol => ol.ProductVariantId == productVariantId);

            // Allocate from inventory items (FIFO)
            foreach (var inventory in availableInventory)
            {
                if (remainingToAllocate <= 0)
                    break;

                var quantityFromThisInventory = Math.Min(remainingToAllocate, inventory.StockQuantity);

                allocations.Add(new OrderLineAllocation(
                    orderLine.Id,
                    0, // Will be set when OrderFulfillment is created
                    productVariantId,
                    quantityFromThisInventory,
                    orderLine.UnitPrice,
                    inventory.Id
                ));

                remainingToAllocate -= quantityFromThisInventory;
            }

            // Update unallocated quantity
            unallocatedItems[productVariantId] -= quantityToAllocate;
        }

        // Only create fulfillment if we allocated some items
        if (allocations.Any())
        {
            var fulfillment = new OrderFulfillment(order.Id, branchInfo.BranchId, (decimal)branchInfo.DistanceKm);

            // Set the fulfillment reference for allocations
            foreach (var allocation in allocations)
            {
                allocation.OrderFulfillmentId = fulfillment.Id; // This will be properly set after saving
                fulfillment.OrderLineAllocations.Add(allocation);
            }

            _logger.LogInformation("Allocated {ItemCount} items from branch {BranchName} (Distance: {Distance:F2}km) for order {OrderId}",
                allocations.Sum(a => a.Quantity), branchInfo.BranchName, branchInfo.DistanceKm, order.Id);

            return fulfillment;
        }

        return null;
    }
}
