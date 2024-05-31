using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public class PaymentMethod : BaseAuditableEntity, ISoftDelete
{
    public PaymentMethod()
    {

    }
    public PaymentMethod(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
}
