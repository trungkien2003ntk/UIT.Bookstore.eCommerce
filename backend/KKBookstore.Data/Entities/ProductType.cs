
namespace KKBookstore.Data.Entities;

public class ProductType : BaseEntity, ITrackable, ISoftDelete
{
    public int ProductTypeID { get; set; }
    public string ProductTypeCode { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public int ParentProductTypeID { get; set; }

    // navigation property
    public ProductType ParentProductType { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
