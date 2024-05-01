namespace KKBookstore.Data.Entities;

public class PaymentMethod: BaseEntity, ITrackable
{
    public int PaymentMethodID { get; set; }
    public string PaymentMethodName { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User LastEditedByUser { get; set; }
}
