using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public class DeliveryMethod : BaseAuditableEntity, ISoftDelete
{
    public DeliveryMethod()
    {

    }
    private DeliveryMethod(
        string name,
        string description
    ) : base()
    {

    }

    public string Name { get; set; }
    public string Description { get; set; }

    // navigation properties
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // factory method
    public static DeliveryMethod Create(
        string name,
        string description
    )
    {
        return new DeliveryMethod(name, description);
    }
}
