using KKBookstore.Models;

namespace KKBookstore.StockTransactions;

public abstract class StockTransaction : BaseFullAuditedEntity
{
    public string Code { get; set; } = null!;
    public DateTimeOffset TransactionDate { get; set; }
    public string? Remarks { get; set; }
    public string? Reason { get; set; }
    public StockTransactionStatus TransactionStatus { get; set; }
    public StockTransactionType TransactionType { get; set; }

    public virtual ICollection<StockTransactionDetail>? Items { get; set; }
}