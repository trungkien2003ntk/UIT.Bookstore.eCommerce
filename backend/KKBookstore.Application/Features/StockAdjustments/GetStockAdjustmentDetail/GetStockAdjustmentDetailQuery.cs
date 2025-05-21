using KKBookstore.Common.Interfaces;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;

public record GetStockAdjustmentDetailQuery(int Id) : IRequest<Result<StockAdjustmentDetail>>;

public class GetStockAdjustmentDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetStockAdjustmentDetailQuery, Result<StockAdjustmentDetail>>
{
    public async Task<Result<StockAdjustmentDetail>> Handle(GetStockAdjustmentDetailQuery request, CancellationToken cancellationToken)
    {
        var stockAdjustment = await dbContext.StockAdjustments
            .AsNoTracking()
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Product)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.ProductVariantOptionValues)!
                        .ThenInclude(vov => vov.OptionValue)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Inventories)
            .Include(sa => sa.Warehouse!)
                .ThenInclude(w => w.Address)
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (stockAdjustment is null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.NotFound", $"Stock adjustment with ID {request.Id} was not found."));
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
                AdjustmentType = item.AdjustmentType,
                Remarks = item.Remarks,
                Reason = item.Reason,
                ThumbnailImageUrl = item.Variant!.Product.GetFirstThumbnailImageUrl() ?? string.Empty,
                VariantName = MappingHelpers.GetProductVariantOptionValuesString(item.Variant),
                LastestUnitCost = item.Variant.LastestUnitCost,
                ProductName = item.Variant.Product.Name
            }).ToList() ?? new List<StockAdjustmentItemDetail>(),
            Warehouse = new WarehouseDetail
            {
                Id = stockAdjustment.Warehouse!.Id,
                Name = stockAdjustment.Warehouse.Name,
                Address = stockAdjustment.Warehouse.Address.ToString(),
                PhoneNumber = stockAdjustment.Warehouse.Address.PhoneNumber,
                Email = stockAdjustment.Warehouse.Email,
                IsDefault = stockAdjustment.Warehouse.IsDefault
            },
            CreationTime = stockAdjustment.CreationTime ?? DateTimeOffset.UtcNow,
            CreatorId = stockAdjustment.CreatorId,
            LastModificationTime = stockAdjustment.LastModificationTime,
            LastModifierId = stockAdjustment.LastModifierId
        };

        return Result.Success(stockAdjustmentDetail);
    }
}

public class StockAdjustmentDetail
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public string? Reason { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int WarehouseId { get; set; }
    public bool IsDeleted { get; set; }
    public List<StockAdjustmentItemDetail> Items { get; set; } = new();
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }

    public WarehouseDetail? Warehouse { get; set; }
}

public class StockAdjustmentItemDetail
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }
    public AdjustmentType AdjustmentType { get; set; }
    public decimal TotalCost => UnitCost * Quantity;

    public string ThumbnailImageUrl { get; set; } = string.Empty;
    public string VariantName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal LastestUnitCost { get; set; } = 0;
}

public class WarehouseDetail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsDefault { get; set; }
}