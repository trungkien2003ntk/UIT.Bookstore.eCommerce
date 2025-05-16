using KKBookstore.Common.Interfaces;
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
            .Include(sa => sa.Items)
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (stockAdjustment is null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.NotFound", $"Stock adjustment with ID {request.Id} was not found."));
        }        var stockAdjustmentDetail = new StockAdjustmentDetail
        {
            Id = stockAdjustment.Id,
            Code = stockAdjustment.Code,
            Remarks = stockAdjustment.Remarks,
            Reason = stockAdjustment.Reason,
            TransactionDate = stockAdjustment.TransactionDate,
            WarehouseId = stockAdjustment.WarehouseId,
            IsDeleted = stockAdjustment.IsDeleted,            Items = stockAdjustment.Items?.Select(item => new StockAdjustmentItemDetail
            {
                Id = item.Id,
                VariantId = item.VariantId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                AdjustmentType = item.AdjustmentType,
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
}