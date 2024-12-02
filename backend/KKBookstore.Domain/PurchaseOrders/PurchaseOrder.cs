using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.PurchaseOrders;

public class PurchaseOrder : BaseAuditedEntity
{
    public int BranchId { get; set; }
    public int SupplierId { get; set; }
    public int PurchaseOrderNumber { get; set; }
    public DateTimeOffset PurchaseOrderDate { get; set; }
    public DateTimeOffset ExpectedDeliveryDate { get; set; }
    public PurchaseOrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
}