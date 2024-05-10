using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class WrittenBy : BaseEntity, ISoftDelete, ITrackable
{
    public int AuthorId { get; set; }
    public int ProductId { get; set; }
    public DateTimeOffset WrittenWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    public Author Author { get; set; }
    public Product Product { get; set; }
}
