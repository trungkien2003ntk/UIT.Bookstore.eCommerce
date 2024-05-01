namespace KKBookstore.Data.Entities;

public class Rating: BaseEntity, ITrackable
{
    public int RatingID { get; set; }
    public string Comment { get; set; }
    public int RatingValue {  get; set; }
    public int UserID { get; set; }
    public int SkuID { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User User { get; set; }
    public Sku Sku { get; set; }
    public User LastEditedByUser { get; set; }
}
