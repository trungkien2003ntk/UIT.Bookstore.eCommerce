using System.ComponentModel.DataAnnotations;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.CreateStockAdjustment;

public record CreateStockAdjustmentCommand : IRequest<Result<StockAdjustmentDetail>>
{
    [Required]
    public string Code { get; init; } = string.Empty;
    
    public DateTimeOffset TransactionDate { get; init; } = DateTimeOffset.UtcNow;
    
    public string? Remarks { get; init; }
    
    public string? Reason { get; init; }
    
    [Required]
    public StockTransactionStatus TransactionStatus { get; init; } = StockTransactionStatus.Pending;
    
    [Required]
    public int WarehouseId { get; init; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required")]
    public List<CreateStockAdjustmentItemCommand> Items { get; init; } = [];
}

public record CreateStockAdjustmentItemCommand
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
    
    [Required]
    public AdjustmentType AdjustmentType { get; init; }
}

public class CreateStockAdjustmentCommandHandler : IRequestHandler<CreateStockAdjustmentCommand, Result<StockAdjustmentDetail>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public CreateStockAdjustmentCommandHandler(
        IApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<StockAdjustmentDetail>> Handle(
        CreateStockAdjustmentCommand request, 
        CancellationToken cancellationToken)
    {
        // Check for duplicate code
        var existingAdjustment = await _dbContext.StockAdjustments
            .AnyAsync(sa => sa.Code == request.Code, cancellationToken);
        
        if (existingAdjustment)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.Conflict("StockAdjustment.CodeAlreadyExists", "A stock adjustment with this code already exists"));
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

        // Validate warehouse exists
        var warehouse = await _dbContext.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.WarehouseId, cancellationToken);

        if (warehouse == null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.WarehouseNotFound", "Warehouse with specified ID was not found"));
        }

        // Create stock adjustment
        var stockAdjustment = new StockAdjustment
        {
            Code = request.Code,
            TransactionDate = request.TransactionDate,
            Remarks = request.Remarks,
            Reason = request.Reason,
            TransactionStatus = request.TransactionStatus,
            TransactionType = StockTransactionType.StockAdjustment,
            WarehouseId = request.WarehouseId
        };        // Create stock adjustment items
        foreach (var item in request.Items)
        {
            var stockAdjustmentItem = new StockAdjustmentItem
            {
                VariantId = item.VariantId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                Reason = item.Reason,
                Remarks = item.Remarks,
                AdjustmentType = item.AdjustmentType,
                StockTransactionId = stockAdjustment.Id
            };
            
            _dbContext.StockAdjustmentItems.Add(stockAdjustmentItem);
        }
        
        _dbContext.StockAdjustments.Add(stockAdjustment);

        // If the status is Completed, update inventory levels based on adjustment items
        if (request.TransactionStatus == StockTransactionStatus.Completed)
        {
            foreach (var item in request.Items)
            {
                // Process each item based on AdjustmentType
                var productVariant = existingVariants[item.VariantId];
                
                if (item.AdjustmentType == AdjustmentType.Increase)
                {
                    // For increases, create a new inventory record
                    var inventory = new Inventory(
                        productVariantId: item.VariantId,
                        initialQuantity: item.Quantity,
                        unitCost: (int)item.UnitCost,
                        isActive: true,
                        warehouseId: request.WarehouseId
                    );
                    
                    _dbContext.Inventories.Add(inventory);
                }
                else if (item.AdjustmentType == AdjustmentType.Decrease)
                {
                    // For decreases, use FIFO to take inventory off
                    // Get active inventories for this product variant at this warehouse
                    var inventories = await _dbContext.Inventories
                        .Where(i => i.ProductVariantId == item.VariantId && i.WarehouseId == request.WarehouseId && i.IsActive && i.StockQuantity > 0)
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
                                $"Insufficient stock for product variant ID {item.VariantId} at warehouse ID {request.WarehouseId}"));
                    }
                }
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        // Return the created adjustment details
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
}