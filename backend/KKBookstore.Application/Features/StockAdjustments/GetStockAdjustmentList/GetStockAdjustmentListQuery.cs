using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Models;
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
        if (request.IsDeleted.HasValue)
        {
            query = query.Where(sa => sa.IsDeleted == request.IsDeleted.Value);
        }

        if (request.WarehouseId.HasValue)
        {
            query = query.Where(sa => sa.WarehouseId == request.WarehouseId.Value);
        }        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
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
    }    private PagedResult<StockAdjustmentSummary> MapToStockAdjustmentSummaryResult(PagedResult<StockAdjustment> paginatedAdjustments)
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
                LastModifierId = sa.LastModifierId
            }).ToList(),
            paginatedAdjustments.TotalCount,
            paginatedAdjustments.PageSize,
            paginatedAdjustments.PageNumber
        );
    }
}

public class StockAdjustmentSummary
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public string? Reason { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int WarehouseId { get; set; }
    public int TotalItems { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }
}