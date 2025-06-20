using KKBookstore.Branches;
using KKBookstore.Models;

namespace KKBookstore.Orders;

/// <summary>
/// Tracks which branch will fulfill which order lines and manages the fulfillment process
/// </summary>
public class OrderFulfillment : BaseAuditedEntity
{
    public int OrderId { get; set; }
    public int BranchId { get; set; }
    public OrderFulfillmentStatus Status { get; set; }
    public decimal DistanceFromCustomer { get; set; } // Distance in kilometers
    public bool IsSelectedForPackaging { get; set; }
    public DateTimeOffset? PackagingStartedWhen { get; set; }
    public DateTimeOffset? PackagingCompletedWhen { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public Branch Branch { get; set; } = null!;
    public ICollection<OrderLineAllocation> OrderLineAllocations { get; set; } = [];

    public OrderFulfillment()
    {
    }

    public OrderFulfillment(int orderId, int branchId, decimal distanceFromCustomer)
    {
        OrderId = orderId;
        BranchId = branchId;
        DistanceFromCustomer = distanceFromCustomer;
        Status = OrderFulfillmentStatus.InventoryAllocated;
        IsSelectedForPackaging = false;
    }

    public void SelectForPackaging()
    {
        IsSelectedForPackaging = true;
        Status = OrderFulfillmentStatus.SelectedForPackaging;
    }

    public void StartPackaging()
    {
        if (!IsSelectedForPackaging)
            throw new InvalidOperationException("Cannot start packaging without being selected first");
        
        Status = OrderFulfillmentStatus.Packaging;
        PackagingStartedWhen = DateTimeOffset.Now;
    }

    public void CompletePackaging()
    {
        if (Status != OrderFulfillmentStatus.Packaging)
            throw new InvalidOperationException("Can only complete packaging when in packaging status");
        
        Status = OrderFulfillmentStatus.ReadyForShipping;
        PackagingCompletedWhen = DateTimeOffset.Now;
    }

    public decimal GetTotalAllocatedValue()
    {
        return OrderLineAllocations.Sum(ola => ola.Quantity * ola.UnitPrice);
    }

    public int GetTotalAllocatedItems()
    {
        return OrderLineAllocations.Sum(ola => ola.Quantity);
    }
}
