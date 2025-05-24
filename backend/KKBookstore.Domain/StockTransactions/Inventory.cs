using KKBookstore.Branches;
using KKBookstore.Models;
using KKBookstore.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.StockTransactions;

public class Inventory : BaseFullAuditedEntity
{
    public int ProductVariantId { get; set; }
    public int? PurchaseOrderLineId { get; set; }
    public int? WarehouseId { get; set; }

    public int InitialQuantity { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }

    public InventorySource SourceType { get; set; } // GRN, Transfer, Adjustment

    // not updatable
    public DateTimeOffset OriginalCreatedDate { get; set; }
    public decimal UnitCost { get; set; }

    public ProductVariant? ProductVariant { get; set; } = null!;
    public Branch? Warehouse { get; set; } = null!;


    #region Calculated properties
    [NotMapped]
    public StockStatus StockStatus => IsActive && StockQuantity > 0 ? StockStatus.InStock : StockStatus.OutOfStock;

    #endregion  

    public Inventory(
        int productVariantId,
        int initialQuantity,
        decimal unitCost,
        bool isActive,
        int? warehouseId = null,
        DateTimeOffset? originalCreatedDate = null,
        int? purchaseOrderLineId = null
    ) : base()
    {
        ProductVariantId = productVariantId;
        PurchaseOrderLineId = purchaseOrderLineId;
        WarehouseId = warehouseId;
        InitialQuantity = initialQuantity;
        StockQuantity = initialQuantity;
        IsActive = isActive;
        OriginalCreatedDate = originalCreatedDate ?? DateTimeOffset.Now;
        UnitCost = unitCost;
    }

    protected Inventory() : base()
    {

    }

    public void Deactivate()
    {
        IsActive = false;
        StockQuantity = 0;
    }
}