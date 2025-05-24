using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            .Include(pv => pv.Inventories)
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
        };
        _dbContext.StockAdjustments.Add(stockAdjustment);
        await _dbContext.SaveChangesAsync();

        foreach (var item in request.Items)
        {

        }

        // If the status is Completed, update inventory levels based on adjustment items
        if (request.TransactionStatus == StockTransactionStatus.Completed)
        {
            foreach (var item in request.Items)
            {
                // Process each item based on AdjustmentType
                var productVariant = existingVariants[item.VariantId];

                var stockAdjustmentItem = new StockAdjustmentItem
                {
                    TotalQuantityBefore = productVariant.Inventories!
                        .Where(i => i.IsActive && i.WarehouseId == request.WarehouseId)
                        .Sum(i => i.StockQuantity),
                    VariantId = item.VariantId,
                    Quantity = item.Quantity,
                    UnitCost = item.UnitCost,
                    Reason = item.Reason,
                    Remarks = item.Remarks,
                    AdjustmentType = item.AdjustmentType,
                    StockTransactionId = stockAdjustment.Id
                };

                _dbContext.StockAdjustmentItems.Add(stockAdjustmentItem);

                if (item.AdjustmentType == AdjustmentType.Increase)
                {
                    // For increases, create a new inventory record
                    var inventory = new Inventory(
                        productVariantId: item.VariantId,
                        initialQuantity: item.Quantity,
                        unitCost: item.UnitCost,
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
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductImages)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.ProductVariantOptionValues)!
                        .ThenInclude(vov => vov.OptionValue)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.ProductVariantOptionValues)!
                        .ThenInclude(vov => vov.Option)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Inventories)
            .Include(sa => sa.Warehouse!)
                .ThenInclude(w => w.Address)
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
            TransactionStatus = stockAdjustment.TransactionStatus,
            TransactionDate = stockAdjustment.TransactionDate,
            WarehouseId = stockAdjustment.WarehouseId,
            IsDeleted = stockAdjustment.IsDeleted,
            Items = stockAdjustment.Items?.Select(item => new StockAdjustmentItemDetail
            {
                Id = item.Id,
                VariantId = item.VariantId,
                TotalQuantityBefore = item.TotalQuantityBefore,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                AdjustmentType = item.AdjustmentType,
                Remarks = item.Remarks,
                Reason = item.Reason,
                ThumbnailImageUrl = item.Variant!.Product.GetFirstThumbnailImageUrl() ?? string.Empty,
                VariantName = MappingHelpers.GetProductVariantOptionValuesString(item.Variant),
                OptionValues = item.Variant.ProductVariantOptionValues?.Select(pov => new ProductVariantOptionDto
                {
                    ProductOptionId = pov.OptionId,
                    ProductOptionValueId = pov.OptionValueId,
                    Name = pov.Option?.Name ?? string.Empty,
                    Value = pov.OptionValue?.Value ?? string.Empty
                }).ToList() ?? [],
                LastestUnitCost = item.Variant.LastestUnitCost,
                ProductName = item.Variant.Product.Name
            }).ToList() ?? [],
            Warehouse = new BranchDetail
            {
                Id = stockAdjustment.Warehouse!.Id,
                Name = stockAdjustment.Warehouse.Name,
                Description = stockAdjustment.Warehouse.Description,
                Email = stockAdjustment.Warehouse.Email,
                IsDefault = stockAdjustment.Warehouse.IsDefault,
                IsDeleted = stockAdjustment.Warehouse.IsDeleted,
                Address = new AddressDetail
                {
                    Id = stockAdjustment.Warehouse.Address.Id,
                    PhoneNumber = stockAdjustment.Warehouse.Address.PhoneNumber,
                    ProvinceId = stockAdjustment.Warehouse.Address.ProvinceId,
                    ProvinceName = stockAdjustment.Warehouse.Address.ProvinceName,
                    DistrictId = stockAdjustment.Warehouse.Address.DistrictId,
                    DistrictName = stockAdjustment.Warehouse.Address.DistrictName,
                    CommuneCode = stockAdjustment.Warehouse.Address.CommuneCode,
                    CommuneName = stockAdjustment.Warehouse.Address.CommuneName,
                    DetailAddress = stockAdjustment.Warehouse.Address.DetailAddress,
                    AddressType = stockAdjustment.Warehouse.Address.Type,
                    FormattedAddress = $"{stockAdjustment.Warehouse.Address.DetailAddress}, {stockAdjustment.Warehouse.Address.CommuneName},  {stockAdjustment.Warehouse.Address.DistrictName}, {stockAdjustment.Warehouse.Address.ProvinceName}"
                },
                CreationTime = stockAdjustment.Warehouse.CreationTime,
                CreatorId = stockAdjustment.Warehouse.CreatorId,
                LastModificationTime = stockAdjustment.Warehouse.LastModificationTime,
                LastModifierId = stockAdjustment.Warehouse.LastModifierId
            }
        };

        return Result.Success(stockAdjustmentDetail);
    }
}