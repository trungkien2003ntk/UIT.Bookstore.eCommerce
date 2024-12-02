using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.PurchaseOrders;

public class GoodsReceivedNote : BaseFullAuditedEntity
{
    public int PurchaseOrderId { get; set; }
    public int WarehouseId { get; set; }
    public DateTimeOffset GoodsReceivedDate { get; set; }
    public string Remarks { get; set; } = null!;
    public GoodsReceivedNoteStatus Status { get; set; }
}