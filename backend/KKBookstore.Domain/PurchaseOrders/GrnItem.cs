using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.PurchaseOrders;

public class GrnItem : BaseFullAuditedEntity
{
    public int GoodsReceivedNoteId { get; set; }
    public int PurchaseOrderItemId { get; set; }
    public int QuantityReceived { get; set; }
    public int QuantityDamaged { get; set; }
    
    public int InspectedByUserId { get; set; }
    public DateTimeOffset InspectionDate { get; set; }
    public QualityStatus QualityStatus { get; set; }
    public string Remarks { get; set; } = null!;
}