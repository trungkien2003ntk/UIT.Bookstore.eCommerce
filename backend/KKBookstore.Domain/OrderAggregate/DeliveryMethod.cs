using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.OrderAggregate;

public class DeliveryMethod : BaseAuditableEntity, ISoftDelete
{
    public DeliveryMethod(
        int id
        ) : base(id)
    {

    }

    public string Name { get; set; }
    public string Description { get; set; }

    // navigation properties
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
}
