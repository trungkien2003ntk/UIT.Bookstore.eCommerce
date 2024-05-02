
namespace KKBookstore.Data.Entities;

public class Author : BaseEntity, ITrackable, ISoftDelete
{
    public int AuthorID { get; set; }
    public string Name {  get; set; }
    public string Description { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation propery
    public ICollection<WrittenBy> AuthorBooks { get; set; } = new List<WrittenBy>();
    public User LastEditedByUser { get; set; }

}
