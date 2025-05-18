using System.ComponentModel.DataAnnotations;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.StockTransfers.GetStockTransferDetail;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockTransfers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockTransfers.CreateStockTransfer;

public record CreateStockTransferCommand : IRequest<Result<StockTransferDetail>>
{
    [Required]
    public string Code { get; init; } = null!;
    
    public DateTimeOffset TransactionDate { get; init; } = DateTimeOffset.UtcNow;
    
    public string? Remarks { get; init; }
    
    public string? Reason { get; init; }
    
    [Required]
    public StockTransactionStatus TransactionStatus { get; init; } = StockTransactionStatus.Pending;
    
    [Required]
    public int SourceWarehouseId { get; init; }
    
    [Required]
    public int DestinationWarehouseId { get; init; }
    
    public StockTransferStatus TransferStatus { get; init; } = StockTransferStatus.None;
    
    public DateTimeOffset? DeparturedDate { get; init; }
    
    public DateTimeOffset? ArrivalDate { get; init; }
    
    public DateTimeOffset? TransferDate { get; init; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<CreateStockTransferItemCommand> Items { get; init; } = [];
}

public record CreateStockTransferItemCommand
{
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

public class CreateStockTransferCommandHandler : IRequestHandler<CreateStockTransferCommand, Result<StockTransferDetail>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public CreateStockTransferCommandHandler(
        IApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<StockTransferDetail>> Handle(
        CreateStockTransferCommand request, 
        CancellationToken cancellationToken)
    {
        // Check for duplicate code
        var existingTransfer = await _dbContext.StockTransfers
            .AnyAsync(st => st.Code == request.Code, cancellationToken);
        
        if (existingTransfer)
        {
            return Result.Failure<StockTransferDetail>(
                Error.Conflict("StockTransfer.CodeAlreadyExists", "A stock transfer with this code already exists"));
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

        // Validate source warehouse exists
        var sourceWarehouse = await _dbContext.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.SourceWarehouseId, cancellationToken);

        if (sourceWarehouse == null)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.SourceWarehouseNotFound", "Source warehouse with specified ID was not found"));
        }

        // Validate destination warehouse exists
        var destinationWarehouse = await _dbContext.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.DestinationWarehouseId, cancellationToken);

        if (destinationWarehouse == null)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.DestinationWarehouseNotFound", "Destination warehouse with specified ID was not found"));
        }

        // Create stock transfer
        var stockTransfer = new StockTransfer
        {
            Code = request.Code,
            TransactionDate = request.TransactionDate,
            Remarks = request.Remarks,
            Reason = request.Reason,
            TransactionStatus = request.TransactionStatus,
            TransactionType = StockTransactionType.StockTransfer,
            SourceWarehouseId = request.SourceWarehouseId,
            DestinationWarehouseId = request.DestinationWarehouseId,
            TransferStatus = request.TransferStatus,
            DeparturedDate = request.DeparturedDate,
            ArrivalDate = request.ArrivalDate,
            TransferDate = request.TransferDate
        };

        // Create stock transfer items
        foreach (var item in request.Items)
        {
            var stockTransferItem = new StockTransferItem
            {
                VariantId = item.VariantId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                Reason = item.Reason,
                Remarks = item.Remarks,
                StockTransactionId = stockTransfer.Id
            };
            
            _dbContext.StockTransferItems.Add(stockTransferItem);
        }
        
        _dbContext.StockTransfers.Add(stockTransfer);

        // If the status is Completed, update inventory levels
        if (request.TransactionStatus == StockTransactionStatus.Completed && 
            request.TransferStatus == StockTransferStatus.Completed)
        {
            foreach (var item in request.Items)
            {
                // Process each item by moving inventory from source to destination
                var productVariant = existingVariants[item.VariantId];
                
                // 1. Decrease inventory at source warehouse (FIFO)
                var sourceInventories = await _dbContext.Inventories
                    .Where(i => i.ProductVariantId == item.VariantId && 
                                i.WarehouseId == request.SourceWarehouseId && 
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
                            $"Insufficient stock for product variant ID {item.VariantId} at source warehouse ID {request.SourceWarehouseId}"));
                }

                // 2. Create new inventory at destination warehouse
                foreach (var (quantity, unitCost) in inventoryUnitsToTransfer)
                {
                    var inventory = new Inventory(
                        productVariantId: item.VariantId,
                        initialQuantity: quantity,
                        unitCost: (int)unitCost,
                        isActive: true,
                        warehouseId: request.DestinationWarehouseId
                    );
                    
                    _dbContext.Inventories.Add(inventory);
                }
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        // Return the created transfer details
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
