
namespace KKBookstore.Data.Entities;

public class SkuImage : BaseEntity, ITrackable, ISoftDelete
{
    public int SkuImageID { get; set; }
    public int SkuID { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation properties
    public Sku Sku { get; set; }
    public User LastEditedByUser { get; set; }
}
