namespace KKBookstore.Data.Entities;

public class Sku : BaseEntity, ITrackable, ISoftDelete
{
    public int SkuID { get; set; }
    public string Barcode { get; set; }
    public int ProductID { get; set; }
    public int Weight { get; set; }
    public string Size { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset ValidTo { get; set; }
    public DateTimeOffset DiscontinuedWhen { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public string Tags { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
