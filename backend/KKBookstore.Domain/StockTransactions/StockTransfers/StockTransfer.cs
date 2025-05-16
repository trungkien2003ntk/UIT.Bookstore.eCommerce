using KKBookstore.Branches;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.StockTransactions.StockTransfers;

public class StockTransfer : StockTransaction
{
    public int SourceWarehouseId { get; set; }
    public int DestinationWarehouseId { get; set; }
    public DateTimeOffset? DeparturedDate { get; set; }
    public DateTimeOffset? ArrivalDate { get; set; }
    public DateTimeOffset? TransferDate { get; set; }
    public StockTransferStatus TransferStatus { get; set; }

    public Branch? SourceWarehouse { get; set; }
    public Branch? DestinationWarehouse { get; set; }
    [NotMapped]
    public new ICollection<StockTransferItem>? Items => base.Items?.OfType<StockTransferItem>().ToList() ?? [];
}
