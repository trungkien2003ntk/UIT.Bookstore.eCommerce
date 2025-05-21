using KKBookstore.Branches;

namespace KKBookstore.StockTransactions.StockAdjustments;

public class StockAdjustment : StockTransaction
{
    public int WarehouseId { get; set; }
    public new ICollection<StockAdjustmentItem>? Items => base.Items?.OfType<StockAdjustmentItem>().ToList() ?? [];
    public Branch? Warehouse { get; set; }
}

