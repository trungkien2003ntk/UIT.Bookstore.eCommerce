namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentList;

using KKBookstore.StockTransactions;

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
    public StockTransactionStatus Status { get; set; }
    public decimal TotalCost { get; set; }
}