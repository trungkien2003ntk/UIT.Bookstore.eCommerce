
namespace KKBookstore.Data.Entities;

public class WrittenBy : BaseEntity, ISoftDelete, ITrackable
{
    public int AuthorID { get; set; }
    public int ProductID { get; set; }
    public DateTimeOffset WrittenWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    public Author Author { get; set; }
    public Product Product { get; set; }
}
