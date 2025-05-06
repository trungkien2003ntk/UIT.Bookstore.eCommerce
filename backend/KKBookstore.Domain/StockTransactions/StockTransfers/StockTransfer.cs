using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.StockTransactions.StockTransfers;

public class StockTransfer : StockTransaction
{
    [NotMapped]
    public new ICollection<StockTransferItem>? Items => base.Items?.OfType<StockTransferItem>().ToList() ?? [];
}
