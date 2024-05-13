using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.OrderAggregate;

public class RefAddressType : BaseAuditableEntity, ISoftDelete
{
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
