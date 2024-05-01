namespace KKBookstore.Data.Entities;

public class DeliveryMethod: BaseEntity, ITrackable
{
    //DeliveryMethodID
    //LastEditedBy
    //LastEditedWhen
    //DeliveryMethodID
    //DeliveryMethodName
    //LastEditedBy
    //LastEditedWhen
    public int DeliveryMethodID { get; set; }
    public string DeliveryMethodName { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
