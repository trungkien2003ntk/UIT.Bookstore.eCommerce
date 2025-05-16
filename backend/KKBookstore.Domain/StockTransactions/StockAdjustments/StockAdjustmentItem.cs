namespace KKBookstore.StockTransactions.StockAdjustments;

public class StockAdjustmentItem : StockTransactionDetail
{
    public AdjustmentType AdjustmentType { get; set; } = AdjustmentType.None;
}

