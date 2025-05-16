using System.ComponentModel.DataAnnotations;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.UpdateStockAdjustment;

public record UpdateStockAdjustmentCommand : IRequest<Result<StockAdjustmentDetail>>
{
    [Required]
    public int Id { get; init; }
    
    public string? Remarks { get; init; }
    
    public string? Reason { get; init; }
    
    [Required]
    public StockTransactionStatus TransactionStatus { get; init; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<UpdateStockAdjustmentItemCommand> Items { get; init; } = [];
}

public record UpdateStockAdjustmentItemCommand
{
    public int? Id { get; init; }
    
    [Required]
    public int VariantId { get; init; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; init; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Unit cost must be greater than 0")]
    public decimal UnitCost { get; init; }
    
    public string? Reason { get; init; }
    
    public string? Remarks { get; init; }
    
    [Required]
    public AdjustmentType AdjustmentType { get; init; }
}

public class UpdateStockAdjustmentCommandHandler : IRequestHandler<UpdateStockAdjustmentCommand, Result<StockAdjustmentDetail>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public UpdateStockAdjustmentCommandHandler(
        IApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<StockAdjustmentDetail>> Handle(
        UpdateStockAdjustmentCommand request, 
        CancellationToken cancellationToken)
    {
        // Get the existing stock adjustment with its items
        var stockAdjustment = await _dbContext.StockAdjustments
            .Include(sa => sa.Items)
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (stockAdjustment == null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.NotFound", "Stock adjustment with specified ID was not found"));
        }

        // Only allow updates if in pending status unless explicitly changing the status
        if (stockAdjustment.TransactionStatus != StockTransactionStatus.Pending && 
            request.TransactionStatus == stockAdjustment.TransactionStatus)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.Validation("StockAdjustment.InvalidStatus", "Only pending stock adjustments can be updated"));
        }

        // Check if status transition is valid
        if (!IsValidStatusTransition(stockAdjustment.TransactionStatus, request.TransactionStatus))
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.Validation("StockAdjustment.InvalidStatusTransition", 
                    $"Cannot change status from {stockAdjustment.TransactionStatus} to {request.TransactionStatus}"));
        }

        // Validate product variants exist
        var variantIds = request.Items.Select(i => i.VariantId).Distinct().ToList();
        var existingVariants = await _dbContext.ProductVariants
            .Where(pv => variantIds.Contains(pv.Id))
            .ToDictionaryAsync(pv => pv.Id, pv => pv, cancellationToken);

        if (existingVariants.Count != variantIds.Count)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.ProductVariantNotFound", "One or more product variants were not found"));
        }

        // Update basic properties
        stockAdjustment.Remarks = request.Remarks;
        stockAdjustment.Reason = request.Reason;
        var previousStatus = stockAdjustment.TransactionStatus;
        stockAdjustment.TransactionStatus = request.TransactionStatus;

        // Track existing item IDs
        var existingItemIds = stockAdjustment.Items?
            .Select(i => i.Id)
            .ToHashSet() ?? new HashSet<int>();
            
        var requestItemIds = request.Items
            .Where(i => i.Id.HasValue)
            .Select(i => i.Id!.Value)
            .ToHashSet();

        // Remove items that are no longer in the request
        var itemsToRemove = stockAdjustment.Items?
            .Where(i => !requestItemIds.Contains(i.Id))
            .ToList();

        if (itemsToRemove != null)
        {
            foreach (var item in itemsToRemove)
            {
                _dbContext.StockAdjustmentItems.Remove((StockAdjustmentItem)item);
            }
        }

        // Update existing items and add new ones
        foreach (var requestItem in request.Items)
        {
            if (requestItem.Id.HasValue && existingItemIds.Contains(requestItem.Id.Value))
            {
                // Update existing item
                var existingItem = stockAdjustment.Items!
                    .First(i => i.Id == requestItem.Id.Value) as StockAdjustmentItem;
                
                existingItem!.VariantId = requestItem.VariantId;
                existingItem.Quantity = requestItem.Quantity;
                existingItem.UnitCost = requestItem.UnitCost;
                existingItem.Reason = requestItem.Reason;
                existingItem.Remarks = requestItem.Remarks;
                existingItem.AdjustmentType = requestItem.AdjustmentType;
            }
            else
            {
                // Add new item
                var newItem = new StockAdjustmentItem
                {
                    VariantId = requestItem.VariantId,
                    StockTransactionId = stockAdjustment.Id,
                    Quantity = requestItem.Quantity,
                    UnitCost = requestItem.UnitCost,
                    Reason = requestItem.Reason,
                    Remarks = requestItem.Remarks,
                    AdjustmentType = requestItem.AdjustmentType
                };
                
                _dbContext.StockAdjustmentItems.Add(newItem);
            }
        }

        // If transitioning from Pending to Completed, process inventory adjustments
        if (previousStatus == StockTransactionStatus.Pending && 
            request.TransactionStatus == StockTransactionStatus.Completed)
        {
            // Load all items again after updates to ensure we have the latest data
            var adjustmentItems = await _dbContext.StockAdjustmentItems
                .Where(i => i.StockTransactionId == stockAdjustment.Id)
                .ToListAsync(cancellationToken);
                
            foreach (var item in adjustmentItems)
            {
                var productVariant = existingVariants[item.VariantId];
                
                if (item.AdjustmentType == AdjustmentType.Increase)
                {
                    // For increases, create a new inventory record
                    var inventory = new Inventory(
                        productVariantId: item.VariantId,
                        initialQuantity: item.Quantity,
                        unitCost: (int)item.UnitCost,
                        isActive: true,
                        warehouseId: stockAdjustment.WarehouseId
                    );
                    
                    _dbContext.Inventories.Add(inventory);
                }
                else if (item.AdjustmentType == AdjustmentType.Decrease)
                {
                    // For decreases, use FIFO to take inventory off
                    var inventories = await _dbContext.Inventories
                        .Where(i => i.ProductVariantId == item.VariantId && 
                                   i.WarehouseId == stockAdjustment.WarehouseId && 
                                   i.IsActive && 
                                   i.StockQuantity > 0)
                        .OrderBy(i => i.OriginalCreatedDate) // FIFO - oldest first
                        .ToListAsync(cancellationToken);

                    // Decrease the stock by the adjustment quantity
                    int remainingToDecrease = item.Quantity;
                    foreach (var inventory in inventories)
                    {
                        if (remainingToDecrease <= 0)
                            break;

                        if (inventory.StockQuantity <= remainingToDecrease)
                        {
                            // Deactivate this inventory item as it will be completely used
                            remainingToDecrease -= inventory.StockQuantity;
                            inventory.Deactivate();
                        }
                        else
                        {
                            // Partially use this inventory item
                            inventory.StockQuantity -= remainingToDecrease;
                            remainingToDecrease = 0;
                        }
                    }

                    // If we couldn't decrease all the quantity, return an error
                    if (remainingToDecrease > 0)
                    {
                        return Result.Failure<StockAdjustmentDetail>(
                            Error.Validation("StockAdjustment.InsufficientStock", 
                                $"Insufficient stock for product variant ID {item.VariantId} at warehouse ID {stockAdjustment.WarehouseId}"));
                    }
                }
            }
        }
        else if (previousStatus == StockTransactionStatus.Completed && 
                 request.TransactionStatus == StockTransactionStatus.Cancelled)
        {
            // If cancelling a completed adjustment, we need to reverse the inventory changes
            // This would be a more complex operation requiring tracking of inventory changes
            // For now, return an error indicating this is not supported
            return Result.Failure<StockAdjustmentDetail>(
                Error.Validation("StockAdjustment.CannotCancel", 
                    "Cannot cancel a completed stock adjustment. Create a new adjustment with opposite types."));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        // Return the updated adjustment details
        return await GetStockAdjustmentDetail(stockAdjustment.Id, cancellationToken);
    }

    private async Task<Result<StockAdjustmentDetail>> GetStockAdjustmentDetail(
        int stockAdjustmentId, 
        CancellationToken cancellationToken)
    {
        var stockAdjustment = await _dbContext.StockAdjustments
            .AsNoTracking()
            .Include(sa => sa.Items)
            .FirstOrDefaultAsync(sa => sa.Id == stockAdjustmentId, cancellationToken);

        if (stockAdjustment is null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.NotFound", $"Stock adjustment with ID {stockAdjustmentId} was not found."));
        }

        var stockAdjustmentDetail = new StockAdjustmentDetail
        {
            Id = stockAdjustment.Id,
            Code = stockAdjustment.Code,
            Remarks = stockAdjustment.Remarks,
            Reason = stockAdjustment.Reason,
            TransactionDate = stockAdjustment.TransactionDate,
            WarehouseId = stockAdjustment.WarehouseId,
            IsDeleted = stockAdjustment.IsDeleted,
            Items = stockAdjustment.Items?.Select(item => new StockAdjustmentItemDetail
            {
                Id = item.Id,
                VariantId = item.VariantId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                AdjustmentType = ((StockAdjustmentItem)item).AdjustmentType,
                Remarks = item.Remarks,
                Reason = item.Reason
            }).ToList() ?? new List<StockAdjustmentItemDetail>(),
            CreationTime = stockAdjustment.CreationTime ?? DateTimeOffset.UtcNow,
            CreatorId = stockAdjustment.CreatorId,
            LastModificationTime = stockAdjustment.LastModificationTime,
            LastModifierId = stockAdjustment.LastModifierId
        };

        return Result.Success(stockAdjustmentDetail);
    }
    
    private bool IsValidStatusTransition(StockTransactionStatus currentStatus, StockTransactionStatus newStatus)
    {
        return (currentStatus, newStatus) switch
        {
            (StockTransactionStatus.Pending, StockTransactionStatus.Completed) => true,
            (StockTransactionStatus.Pending, StockTransactionStatus.Cancelled) => true,
            (_, _) when currentStatus == newStatus => true, // Allow same status
            _ => false
        };
    }
}