using KKBookstore.Common.Interfaces;
using KKBookstore.Features.StockTransfers.GetStockTransferDetail;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockTransfers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Features.StockTransfers.UpdateStockTransfer;

public record UpdateStockTransferCommand : IRequest<Result<StockTransferDetail>>
{
    [Required]
    public int Id { get; init; }

    public string? Remarks { get; init; }

    public string? Reason { get; init; }

    [Required]
    public StockTransactionStatus TransactionStatus { get; init; }

    [Required]
    public StockTransferStatus TransferStatus { get; init; }

    public DateTimeOffset? DeparturedDate { get; init; }

    public DateTimeOffset? ArrivalDate { get; init; }

    public DateTimeOffset? TransferDate { get; init; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<UpdateStockTransferItemCommand> Items { get; init; } = [];
}

public record UpdateStockTransferItemCommand
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
}

public class UpdateStockTransferCommandHandler : IRequestHandler<UpdateStockTransferCommand, Result<StockTransferDetail>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public UpdateStockTransferCommandHandler(
        IApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<StockTransferDetail>> Handle(
        UpdateStockTransferCommand request,
        CancellationToken cancellationToken)
    {
        var stockTransfer = await _dbContext.StockTransfers
            .Include(st => st.Items)
            .FirstOrDefaultAsync(st => st.Id == request.Id, cancellationToken);

        if (stockTransfer is null)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.NotFound", $"Stock transfer with ID {request.Id} was not found."));
        }

        // Only allow updates if in pending status unless explicitly changing the status
        if (stockTransfer.TransactionStatus != StockTransactionStatus.Pending &&
            request.TransactionStatus == stockTransfer.TransactionStatus &&
            request.TransferStatus == stockTransfer.TransferStatus)
        {
            return Result.Failure<StockTransferDetail>(
                Error.Validation("StockTransfer.InvalidStatus", "Only pending stock transfers can be updated."));
        }

        // Validate product variants exist
        var variantIds = request.Items.Select(i => i.VariantId).Distinct().ToList();
        var existingVariants = await _dbContext.ProductVariants
            .Where(pv => variantIds.Contains(pv.Id))
            .ToDictionaryAsync(pv => pv.Id, pv => pv, cancellationToken);

        if (existingVariants.Count != variantIds.Count)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.ProductVariantNotFound", "One or more product variants were not found"));
        }

        // Update basic properties
        stockTransfer.Remarks = request.Remarks;
        stockTransfer.Reason = request.Reason;
        stockTransfer.TransactionStatus = request.TransactionStatus;
        stockTransfer.TransferStatus = request.TransferStatus;
        stockTransfer.DeparturedDate = request.DeparturedDate;
        stockTransfer.ArrivalDate = request.ArrivalDate;
        stockTransfer.TransferDate = request.TransferDate;

        // Track existing item IDs
        var existingItemIds = stockTransfer.Items?.Select(i => i.Id).ToHashSet() ?? new HashSet<int>();
        var requestItemIds = request.Items
            .Where(i => i.Id.HasValue)
            .Select(i => i.Id!.Value)
            .ToHashSet();

        // Remove items that are no longer in the request
        var itemsToRemove = stockTransfer.Items?
            .Where(i => !requestItemIds.Contains(i.Id))
            .ToList();

        if (itemsToRemove != null)
        {
            foreach (var item in itemsToRemove)
            {
                _dbContext.StockTransferItems.Remove((StockTransferItem)item);
            }
        }

        // Update existing items and add new ones
        foreach (var requestItem in request.Items)
        {
            if (requestItem.Id.HasValue && existingItemIds.Contains(requestItem.Id.Value))
            {
                // Update existing item
                var existingItem = stockTransfer.Items!
                    .First(i => i.Id == requestItem.Id.Value) as StockTransferItem;

                existingItem!.VariantId = requestItem.VariantId;
                existingItem.Quantity = requestItem.Quantity;
                existingItem.UnitCost = requestItem.UnitCost;
                existingItem.Reason = requestItem.Reason;
                existingItem.Remarks = requestItem.Remarks;
            }
            else
            {
                // Add new item
                var newItem = new StockTransferItem
                {
                    StockTransactionId = stockTransfer.Id,
                    VariantId = requestItem.VariantId,
                    Quantity = requestItem.Quantity,
                    UnitCost = requestItem.UnitCost,
                    Reason = requestItem.Reason,
                    Remarks = requestItem.Remarks,
                };

                _dbContext.StockTransferItems.Add(newItem);
            }
        }

        // If the status is being changed to Completed, update inventory
        bool isBeingCompleted = stockTransfer.TransactionStatus != StockTransactionStatus.Completed &&
                               request.TransactionStatus == StockTransactionStatus.Completed &&
                               request.TransferStatus == StockTransferStatus.Completed;

        if (isBeingCompleted)
        {
            // Process inventory transfer from source to destination warehouse
            foreach (var item in request.Items)
            {
                // 1. Decrease inventory at source warehouse (FIFO)
                var sourceInventories = await _dbContext.Inventories
                    .Where(i => i.ProductVariantId == item.VariantId &&
                                i.WarehouseId == stockTransfer.SourceWarehouseId &&
                                i.IsActive &&
                                i.StockQuantity > 0)
                    .OrderBy(i => i.OriginalCreatedDate) // FIFO - oldest first
                    .ToListAsync(cancellationToken);

                // Keep track of units to move and their original costs
                var inventoryUnitsToTransfer = new List<(int quantity, decimal unitCost)>();
                int remainingToDecrease = item.Quantity;

                // Decrease inventory at source warehouse
                foreach (var inventory in sourceInventories)
                {
                    if (remainingToDecrease <= 0)
                        break;

                    if (inventory.StockQuantity <= remainingToDecrease)
                    {
                        // Take all units from this inventory item
                        inventoryUnitsToTransfer.Add((inventory.StockQuantity, inventory.UnitCost));
                        remainingToDecrease -= inventory.StockQuantity;
                        inventory.Deactivate();
                    }
                    else
                    {
                        // Take part of this inventory item
                        inventoryUnitsToTransfer.Add((remainingToDecrease, inventory.UnitCost));
                        inventory.StockQuantity -= remainingToDecrease;
                        remainingToDecrease = 0;
                    }
                }

                // If we couldn't decrease all the quantity, return an error
                if (remainingToDecrease > 0)
                {
                    return Result.Failure<StockTransferDetail>(
                        Error.Validation("StockTransfer.InsufficientStock",
                            $"Insufficient stock for product variant ID {item.VariantId} at source warehouse ID {stockTransfer.SourceWarehouseId}"));
                }

                // 2. Create new inventory at destination warehouse
                foreach (var (quantity, unitCost) in inventoryUnitsToTransfer)
                {
                    var inventory = new Inventory(
                        productVariantId: item.VariantId,
                        initialQuantity: quantity,
                        unitCost: (int)unitCost,
                        isActive: true,
                        warehouseId: stockTransfer.DestinationWarehouseId
                    );

                    _dbContext.Inventories.Add(inventory);
                }
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        // Return the updated transfer detail
        return await GetStockTransferDetail(stockTransfer.Id, cancellationToken);
    }

    private async Task<Result<StockTransferDetail>> GetStockTransferDetail(
        int stockTransferId,
        CancellationToken cancellationToken)
    {
        var stockTransfer = await _dbContext.StockTransfers
            .AsNoTracking()
            .Include(st => st.Items)
            .Include(st => st.SourceWarehouse)
            .Include(st => st.DestinationWarehouse)
            .FirstOrDefaultAsync(st => st.Id == stockTransferId, cancellationToken);

        if (stockTransfer is null)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.NotFound", $"Stock transfer with ID {stockTransferId} was not found."));
        }

        var stockTransferDetail = new StockTransferDetail
        {
            Id = stockTransfer.Id,
            Code = stockTransfer.Code,
            Remarks = stockTransfer.Remarks,
            Reason = stockTransfer.Reason,
            TransactionDate = stockTransfer.TransactionDate,
            SourceWarehouseId = stockTransfer.SourceWarehouseId,
            SourceWarehouseName = stockTransfer.SourceWarehouse?.Name,
            DestinationWarehouseId = stockTransfer.DestinationWarehouseId,
            DestinationWarehouseName = stockTransfer.DestinationWarehouse?.Name,
            TransferStatus = stockTransfer.TransferStatus,
            DeparturedDate = stockTransfer.DeparturedDate,
            ArrivalDate = stockTransfer.ArrivalDate,
            TransferDate = stockTransfer.TransferDate,
            IsDeleted = stockTransfer.IsDeleted,
            Items = stockTransfer.Items?.Select(item => new StockTransferItemDetail
            {
                Id = item.Id,
                VariantId = item.VariantId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                Remarks = item.Remarks,
                Reason = item.Reason
            }).ToList() ?? new List<StockTransferItemDetail>(),
            CreationTime = stockTransfer.CreationTime ?? DateTimeOffset.UtcNow,
            CreatorId = stockTransfer.CreatorId,
            LastModificationTime = stockTransfer.LastModificationTime,
            LastModifierId = stockTransfer.LastModifierId
        };

        return Result.Success(stockTransferDetail);
    }
}
