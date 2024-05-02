
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KKBookstore.Data.Entities;

public class Product : BaseEntity, ITrackable, ISoftDelete
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public int ProductTypeID { get; set; }
    public string Description { get; set; }
    public bool HasAuthor {  get; set; }
    public bool IsActive { get; set; }
    public string UnitMeasureCode { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public ProductType ProductType { get; set; }
    public UnitMeasure UnitMeasure { get; set; }
    public User LastEditedByUser { get; set; }
    public ICollection<WrittenBy>? BookAuthors { get; set; } = new List<WrittenBy>();
}
