using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate;

public class Author : BaseAuditableEntity, ISoftDelete
{
    public Author(
        int id,
        string name,
        string description
        ) : base(id)
    {
        Name = name;
        Description = description;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation propery
    public ICollection<WrittenBy> AuthorBooks { get; set; } = new List<WrittenBy>();

}
