namespace KKBookstore.Orders;

public enum OrderFulfillmentStatus
{
    InventoryAllocated,     // Inventory has been allocated to this branch
    SelectedForPackaging,   // Admin has selected this branch for packaging
    Packaging,              // Packaging is in progress
    ReadyForShipping,       // Packaging completed, ready for pickup
    Shipped,                // Items have been shipped
    Delivered               // Items have been delivered
}
