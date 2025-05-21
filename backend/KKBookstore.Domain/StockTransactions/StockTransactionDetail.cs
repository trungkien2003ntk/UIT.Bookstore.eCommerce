using KKBookstore.Models;
using KKBookstore.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.StockTransactions;

public abstract class StockTransactionDetail : BaseFullAuditedEntity
{
    public int VariantId { get; set; }
    public int StockTransactionId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }

    [NotMapped]
    public decimal TotalCost => UnitCost * Quantity;

    public ProductVariant? Variant { get; set; }
}

