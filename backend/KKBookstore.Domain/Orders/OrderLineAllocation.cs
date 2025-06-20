using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.StockTransactions;

namespace KKBookstore.Orders;

/// <summary>
/// Tracks which specific order line items are allocated to which branch for fulfillment
/// </summary>
public class OrderLineAllocation : BaseEntity
{
    public int OrderLineId { get; set; }
    public int OrderFulfillmentId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int InventoryId { get; set; } // Which specific inventory item was used (for FIFO tracking)

    // Navigation properties
    public OrderLine OrderLine { get; set; } = null!;
    public OrderFulfillment OrderFulfillment { get; set; } = null!;
    public ProductVariant ProductVariant { get; set; } = null!;
    public Inventory Inventory { get; set; } = null!;

    public OrderLineAllocation()
    {
    }

    public OrderLineAllocation(
        int orderLineId, 
        int orderFulfillmentId, 
        int productVariantId, 
        int quantity, 
        decimal unitPrice, 
        int inventoryId)
    {
        OrderLineId = orderLineId;
        OrderFulfillmentId = orderFulfillmentId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        InventoryId = inventoryId;
    }

    public decimal GetTotalValue() => Quantity * UnitPrice;
}
