using KKBookstore.Common.Interfaces;
using KKBookstore.Extensions;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentSummary;

public record GetStockAdjustmentSummaryQuery : IRequest<Result<StockAdjustmentSummaryReport>>
{
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}

public class GetStockAdjustmentSummaryQueryHandler : IRequestHandler<GetStockAdjustmentSummaryQuery, Result<StockAdjustmentSummaryReport>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetStockAdjustmentSummaryQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<StockAdjustmentSummaryReport>> Handle(
        GetStockAdjustmentSummaryQuery request,
        CancellationToken cancellationToken)
    {
        // Query all stock adjustments and their items
        var adjustmentsWithItems = await _dbContext.StockAdjustments
            .AsNoTracking()
            .WhereIf(request.StartDate.HasValue, sa => sa.TransactionDate >= request.StartDate!.Value)
            .WhereIf(request.EndDate.HasValue, sa => sa.TransactionDate <= request.EndDate!.Value)
            .Include(sa => sa.Items)
            .ToListAsync(cancellationToken);

        // Group by status and calculate amounts
        var total = CalculateSummary(adjustmentsWithItems);
        var completed = CalculateSummary(adjustmentsWithItems.Where(sa => sa.TransactionStatus == StockTransactionStatus.Completed));
        var pending = CalculateSummary(adjustmentsWithItems.Where(sa => sa.TransactionStatus == StockTransactionStatus.Pending));
        var cancelled = CalculateSummary(adjustmentsWithItems.Where(sa => sa.TransactionStatus == StockTransactionStatus.Cancelled));

        var summary = new StockAdjustmentSummaryReport
        {
            // Total counts
            TotalCount = adjustmentsWithItems.Count,
            TotalIncreasedAmount = total.increasedAmount,
            TotalDecreasedAmount = total.decreasedAmount,

            // Completed counts
            CompletedCount = adjustmentsWithItems.Count(sa => sa.TransactionStatus == StockTransactionStatus.Completed),
            CompletedIncreasedAmount = completed.increasedAmount,
            CompletedDecreasedAmount = completed.decreasedAmount,

            // Pending counts
            PendingCount = adjustmentsWithItems.Count(sa => sa.TransactionStatus == StockTransactionStatus.Pending),
            PendingIncreasedAmount = pending.increasedAmount,
            PendingDecreasedAmount = pending.decreasedAmount,

            // Cancelled counts
            CancelledCount = adjustmentsWithItems.Count(sa => sa.TransactionStatus == StockTransactionStatus.Cancelled),
            CancelledIncreasedAmount = cancelled.increasedAmount,
            CancelledDecreasedAmount = cancelled.decreasedAmount
        };

        return Result.Success(summary);
    }

    private static (decimal increasedAmount, decimal decreasedAmount) CalculateSummary(IEnumerable<StockAdjustment> adjustments)
    {
        decimal increasedAmount = 0;
        decimal decreasedAmount = 0;

        foreach (var adjustment in adjustments)
        {
            if (adjustment.Items == null) continue;

            foreach (var item in adjustment.Items)
            {
                decimal itemTotal = item.Quantity * item.UnitCost;

                if (item.AdjustmentType == AdjustmentType.Increase)
                {
                    increasedAmount += itemTotal;
                }
                else if (item.AdjustmentType == AdjustmentType.Decrease)
                {
                    decreasedAmount += itemTotal;
                }
            }
        }

        return (increasedAmount, decreasedAmount);
    }
}
