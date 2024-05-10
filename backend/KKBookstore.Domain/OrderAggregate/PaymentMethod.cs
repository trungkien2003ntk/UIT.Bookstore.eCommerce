using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.OrderAggregate;

public class PaymentMethod : BaseEntity, ITrackable
{
    public int PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User LastEditedByUser { get; set; }
}
