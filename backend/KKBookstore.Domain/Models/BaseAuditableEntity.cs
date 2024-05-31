using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Interfaces;

namespace KKBookstore.Domain.Models;

public abstract class BaseAuditableEntity : BaseEntity, ITrackable
{
    protected BaseAuditableEntity(int id) : base(id)
    {
    }

    protected BaseAuditableEntity() : base()
    {

    }

    public DateTimeOffset CreatedWhen { get; set; }
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; }

    public DateTimeOffset LastEditedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
}
