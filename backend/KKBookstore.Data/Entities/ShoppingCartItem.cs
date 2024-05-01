
namespace KKBookstore.Data.Entities;

public class ShoppingCartItem : BaseEntity, ITrackable
{
    public int ShoppingCartItemID { get; set; }
    public int CustomerID { get; set; }
    public int SkuID { get; set; }
    public int Quantity { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
