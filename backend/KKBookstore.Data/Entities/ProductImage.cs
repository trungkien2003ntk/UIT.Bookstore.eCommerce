
namespace KKBookstore.Data.Entities;

public class ProductImage : BaseEntity, ITrackable, ISoftDelete
{
    public int ProductImageID { get; set; }
    public int ProductID { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
