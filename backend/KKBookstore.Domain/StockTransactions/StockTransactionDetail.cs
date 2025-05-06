using KKBookstore.Models;

namespace KKBookstore.StockTransactions;

public abstract class StockTransactionDetail : BaseFullAuditedEntity
{
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
}

