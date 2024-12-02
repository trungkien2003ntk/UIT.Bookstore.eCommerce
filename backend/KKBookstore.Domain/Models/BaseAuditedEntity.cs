using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Models;

public abstract class BaseAuditedEntity : BaseEntity, IAuditedObject
{
    public DateTimeOffset? CreationTime { get; set; }
    
    public int? CreatorId { get; set; }
    
    public User? Creator { get; set; }
    
    public int? LastModifierId { get; set; }
    
    public User? LastModifier { get; set; }
    
    public DateTimeOffset? LastModificationTime { get; set; }

    
    protected BaseAuditedEntity(int id) : base(id)
    {
    }

    
    protected BaseAuditedEntity() : base()
    {
    }
}
