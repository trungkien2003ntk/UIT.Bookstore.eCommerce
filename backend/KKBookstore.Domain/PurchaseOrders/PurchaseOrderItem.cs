using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.PurchaseOrders;

public class PurchaseOrderItem : BaseAuditedEntity
{
    public int PurchaseOrderId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
}