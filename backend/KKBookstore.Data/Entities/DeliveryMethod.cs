namespace KKBookstore.Data.Entities;

public class DeliveryMethod : BaseEntity, ITrackable, ISoftDelete
{
    public int DeliveryMethodID { get; set; }
    public string DeliveryMethodName { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation properties
    public User LastEditedByUser { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
