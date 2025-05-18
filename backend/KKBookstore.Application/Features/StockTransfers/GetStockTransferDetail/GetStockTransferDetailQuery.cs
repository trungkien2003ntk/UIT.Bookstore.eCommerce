using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.StockTransactions.StockTransfers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockTransfers.GetStockTransferDetail;

public record GetStockTransferDetailQuery(int Id) : IRequest<Result<StockTransferDetail>>;

public class GetStockTransferDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetStockTransferDetailQuery, Result<StockTransferDetail>>
{
    public async Task<Result<StockTransferDetail>> Handle(GetStockTransferDetailQuery request, CancellationToken cancellationToken)
    {
        var stockTransfer = await dbContext.StockTransfers
            .AsNoTracking()
            .Include(st => st.Items)
            .Include(st => st.SourceWarehouse)
            .Include(st => st.DestinationWarehouse)
            .FirstOrDefaultAsync(st => st.Id == request.Id, cancellationToken);

        if (stockTransfer is null)
        {
            return Result.Failure<StockTransferDetail>(
                Error.NotFound("StockTransfer.NotFound", $"Stock transfer with ID {request.Id} was not found."));
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

public class StockTransferDetail
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public string? Reason { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int SourceWarehouseId { get; set; }
    public string? SourceWarehouseName { get; set; }
    public int DestinationWarehouseId { get; set; }
    public string? DestinationWarehouseName { get; set; }
    public StockTransferStatus TransferStatus { get; set; }
    public DateTimeOffset? DeparturedDate { get; set; }
    public DateTimeOffset? ArrivalDate { get; set; }
    public DateTimeOffset? TransferDate { get; set; }
    public bool IsDeleted { get; set; }
    public List<StockTransferItemDetail> Items { get; set; } = new();
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }
}

public class StockTransferItemDetail
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }
    public decimal TotalCost => UnitCost * Quantity;
}
