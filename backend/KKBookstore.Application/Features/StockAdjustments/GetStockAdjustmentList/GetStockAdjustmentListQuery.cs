using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentList;

public record GetStockAdjustmentListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<StockAdjustmentSummary>>>
{
    public bool? IsDeleted { get; set; }
    public int? WarehouseId { get; set; }
    public string? SearchQuery { get; set; }
    public StockTransactionStatus? TransactionStatus { get; set; }
    public DateTimeOffset? TransactionDateFrom { get; set; }
    public DateTimeOffset? TransactionDateTo { get; set; }
}

public class GetStockAdjustmentListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetStockAdjustmentListQuery, Result<PagedResult<StockAdjustmentSummary>>>
{
    public async Task<Result<PagedResult<StockAdjustmentSummary>>> Handle(GetStockAdjustmentListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<StockAdjustment> query = dbContext.StockAdjustments
            .AsNoTracking()
            .Include(sa => sa.Items);

        // Apply filters
        query = query
            .WhereIf(request.IsDeleted.HasValue, sa => sa.IsDeleted == request.IsDeleted!.Value)
            .WhereIf(request.WarehouseId.HasValue, sa => sa.WarehouseId == request.WarehouseId!.Value)
            .WhereIf(request.TransactionStatus.HasValue, sa => sa.TransactionStatus == request.TransactionStatus!.Value)
            .WhereIf(request.TransactionDateFrom.HasValue, sa => sa.TransactionDate.Date >= request.TransactionDateFrom!.Value.Date)
            .WhereIf(request.TransactionDateTo.HasValue, sa => sa.TransactionDate.Date <= request.TransactionDateTo!.Value.Date);

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.ToLower().Trim();
            query = query.Where(sa =>
                sa.Code.ToLower().Contains(searchQuery) ||
                (sa.Remarks != null && sa.Remarks.ToLower().Contains(searchQuery)) ||
                (sa.Reason != null && sa.Reason.ToLower().Contains(searchQuery)));
        }

        // Apply sorting
        var validSortProperties = new List<string>
        {            nameof(StockAdjustment.Id),
            nameof(StockAdjustment.Code),
            nameof(StockAdjustment.TransactionDate),
            nameof(StockAdjustment.WarehouseId),
            nameof(StockAdjustment.CreationTime)
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
            return Result.Failure<PagedResult<StockAdjustmentSummary>>(sortAndPagingResult.Error);
        }

        var paginatedAdjustments = sortAndPagingResult.Value;

        if (paginatedAdjustments.Items.Count == 0)
        {
            return Result.Failure<PagedResult<StockAdjustmentSummary>>(
                Error.NotFound("StockAdjustment.NotFound", "No stock adjustments were found."));
        }

        var mappedPaginatedAdjustments = MapToStockAdjustmentSummaryResult(paginatedAdjustments);

        return Result.Success(mappedPaginatedAdjustments);
    }
    private PagedResult<StockAdjustmentSummary> MapToStockAdjustmentSummaryResult(PagedResult<StockAdjustment> paginatedAdjustments)
    {
        return new PagedResult<StockAdjustmentSummary>(
            paginatedAdjustments.Items.Select(sa => new StockAdjustmentSummary
            {
                Id = sa.Id,
                Code = sa.Code,
                Remarks = sa.Remarks,
                Reason = sa.Reason,
                TransactionDate = sa.TransactionDate,
                WarehouseId = sa.WarehouseId,
                TotalItems = sa.Items?.Count ?? 0,
                IsDeleted = sa.IsDeleted,
                CreationTime = sa.CreationTime ?? DateTimeOffset.UtcNow,
                CreatorId = sa.CreatorId,
                LastModificationTime = sa.LastModificationTime,
                LastModifierId = sa.LastModifierId,
                TransactionStatus = sa.TransactionStatus,
                TotalCost = sa.Items?.Sum(item => item.Quantity * item.UnitCost) ?? 0
            }).ToList(),
            paginatedAdjustments.TotalCount,
            paginatedAdjustments.PageSize,
            paginatedAdjustments.PageNumber
        );
    }
}
