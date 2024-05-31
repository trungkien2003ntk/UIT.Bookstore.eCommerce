using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public class RefAddressType : BaseAuditableEntity, ISoftDelete
{
    public RefAddressType()
    {

    }
    public RefAddressType(
        string name,
        string description
    ) : base()
    {
        Name = name;
        Description = description;
        IsDeleted = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
}
