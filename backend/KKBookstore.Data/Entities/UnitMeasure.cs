
namespace KKBookstore.Data.Entities;

public class UnitMeasure : BaseEntity, ITrackable, ISoftDelete
{
    public string UnitMeasureCode { get; set; }
    public string Description {  get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
}
