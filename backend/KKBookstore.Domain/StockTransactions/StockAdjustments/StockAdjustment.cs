namespace KKBookstore.StockTransactions.StockAdjustments;

public class StockAdjustment : StockTransaction
{
    public int WarehouseId { get; set; }
    public new ICollection<StockAdjustmentItem>? Items => base.Items?.OfType<StockAdjustmentItem>().ToList() ?? [];
}

