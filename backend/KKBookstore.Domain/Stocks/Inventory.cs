using KKBookstore.Domain.Branches;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Stocks;

public class Inventory : BaseFullAuditedEntity
{
    public int ProductVariantId { get; set; }
    public int? PurchaseOrderLineId { get; set; }
    public int? WarehouseId { get; set; }
    public int StockQuantity { get; set; }

    // not updatable
    public int PurchasePrice { get; set; }
    public bool IsActive { get; set; }

    public ProductVariant ProductVariant { get; set; } = null!;
    public Branch Warehouse { get; set; } = null!;

    [NotMapped]
    public StockStatus StockStatus => IsActive && StockQuantity > 0 ? StockStatus.InStock : StockStatus.OutOfStock;

    public Inventory() : base()
    {

    }
}