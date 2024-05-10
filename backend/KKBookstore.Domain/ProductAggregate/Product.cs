using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KKBookstore.Domain.ProductAggregate;

public class Product : BaseEntity, ITrackable, ISoftDelete
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public string Description { get; set; }
    public bool HasAuthor { get; set; }
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
    public ICollection<Sku> Skus { get; set; } = new List<Sku>();
    public ICollection<WrittenBy>? BookAuthors { get; set; } = new List<WrittenBy>();
}
