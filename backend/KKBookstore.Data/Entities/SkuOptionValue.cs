
namespace KKBookstore.Data.Entities;

public class SkuOptionValue : BaseEntity, ISoftDelete, ITrackable
{
    public int SkuID { get; set; }
    public int OptionID { get; set; }
    public int OptionValueID { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
