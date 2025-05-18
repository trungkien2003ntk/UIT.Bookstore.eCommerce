using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Models;
using KKBookstore.StockTransactions.StockTransfers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockTransfers.GetStockTransferList;

public record GetStockTransferListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<StockTransferSummary>>>
{
    public bool? IsDeleted { get; set; }
    public int? SourceWarehouseId { get; set; }
    public int? DestinationWarehouseId { get; set; }
    public StockTransferStatus? TransferStatus { get; set; }
    public string? SearchQuery { get; set; }
}

public class GetStockTransferListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetStockTransferListQuery, Result<PagedResult<StockTransferSummary>>>
{
    public async Task<Result<PagedResult<StockTransferSummary>>> Handle(GetStockTransferListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<StockTransfer> query = dbContext.StockTransfers
            .AsNoTracking()
            .Include(st => st.Items)
            .Include(st => st.SourceWarehouse)
            .Include(st => st.DestinationWarehouse);

        // Apply filters
        if (request.IsDeleted.HasValue)
        {
            query = query.Where(st => st.IsDeleted == request.IsDeleted.Value);
        }

        if (request.SourceWarehouseId.HasValue)
        {
            query = query.Where(st => st.SourceWarehouseId == request.SourceWarehouseId.Value);
        }

        if (request.DestinationWarehouseId.HasValue)
        {
            query = query.Where(st => st.DestinationWarehouseId == request.DestinationWarehouseId.Value);
        }

        if (request.TransferStatus.HasValue)
        {
            query = query.Where(st => st.TransferStatus == request.TransferStatus.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.ToLower().Trim();
            query = query.Where(st =>
                st.Code.ToLower().Contains(searchQuery) ||
                (st.Remarks != null && st.Remarks.ToLower().Contains(searchQuery)) ||
                (st.Reason != null && st.Reason.ToLower().Contains(searchQuery)));
        }

        // Apply sorting
        var validSortProperties = new List<string>
        {
            nameof(StockTransfer.Id),
            nameof(StockTransfer.Code),
            nameof(StockTransfer.TransactionDate),
            nameof(StockTransfer.SourceWarehouseId),
            nameof(StockTransfer.DestinationWarehouseId),
            nameof(StockTransfer.CreationTime)
        };

        var sortAndPagingResult = await query.SortAndPaginateWithResultAsync(
            request.SortBy,
            request.SortDirection,
            validSortProperties,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (sortAndPagingResult.IsFailure)
        {
            return Result.Failure<PagedResult<StockTransferSummary>>(sortAndPagingResult.Error);
        }

        var paginatedTransfers = sortAndPagingResult.Value;

        if (paginatedTransfers.Items.Count == 0)
        {
            return Result.Failure<PagedResult<StockTransferSummary>>(
                Error.NotFound("StockTransfer.NotFound", "No stock transfers were found."));
        }

        var mappedPaginatedTransfers = MapToStockTransferSummaryResult(paginatedTransfers);

        return Result.Success(mappedPaginatedTransfers);
    }

    private PagedResult<StockTransferSummary> MapToStockTransferSummaryResult(PagedResult<StockTransfer> paginatedTransfers)
    {
        return new PagedResult<StockTransferSummary>(
            paginatedTransfers.Items.Select(st => new StockTransferSummary
            {
                Id = st.Id,
                Code = st.Code,
                Remarks = st.Remarks,
                Reason = st.Reason,
                TransactionDate = st.TransactionDate,
                SourceWarehouseId = st.SourceWarehouseId,
                SourceWarehouseName = st.SourceWarehouse?.Name,
                DestinationWarehouseId = st.DestinationWarehouseId,
                DestinationWarehouseName = st.DestinationWarehouse?.Name,
                TransferStatus = st.TransferStatus,
                DeparturedDate = st.DeparturedDate,
                ArrivalDate = st.ArrivalDate,
                TransferDate = st.TransferDate,
                TotalItems = st.Items?.Count ?? 0,
                IsDeleted = st.IsDeleted,
                CreationTime = st.CreationTime ?? DateTimeOffset.UtcNow,
                CreatorId = st.CreatorId,
                LastModificationTime = st.LastModificationTime,
                LastModifierId = st.LastModifierId
            }).ToList(),
            paginatedTransfers.TotalCount,
            paginatedTransfers.PageSize,
            paginatedTransfers.PageNumber
        );
    }
}

public class StockTransferSummary
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
    public int TotalItems { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }
}
