using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    protected BaseAuditableEntity(int id) : base(id)
    {
    }

    public DateTimeOffset CreatedWhen { get; set; }
    public DateTimeOffset CreatedBy { get; set; }
    public int LastEditedBy { get; set; }
    public User LasteditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
