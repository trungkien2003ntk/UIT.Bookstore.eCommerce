namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentSummary;

public record StockAdjustmentSummaryReport
{
    public int TotalCount { get; init; }
    public decimal TotalIncreasedAmount { get; init; }
    public decimal TotalDecreasedAmount { get; init; }
    
    public int CompletedCount { get; init; }
    public decimal CompletedIncreasedAmount { get; init; }
    public decimal CompletedDecreasedAmount { get; init; }
    
    public int PendingCount { get; init; }
    public decimal PendingIncreasedAmount { get; init; }
    public decimal PendingDecreasedAmount { get; init; }
    
    public int CancelledCount { get; init; }
    public decimal CancelledIncreasedAmount { get; init; }
    public decimal CancelledDecreasedAmount { get; init; }
}
