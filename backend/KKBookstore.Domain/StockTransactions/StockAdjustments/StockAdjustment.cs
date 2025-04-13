namespace KKBookstore.StockTransactions.StockAdjustments;

public class StockAdjustment : StockTransaction
{
    public string? Reason { get; set; }

    public new ICollection<StockAdjustmentItem>? Items => base.Items?.OfType<StockAdjustmentItem>().ToList() ?? [];
}

