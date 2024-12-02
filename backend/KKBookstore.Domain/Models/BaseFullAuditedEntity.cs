using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Models;

public class BaseFullAuditedEntity : BaseAuditedEntity, IFullAuditedObject
{
    public bool IsDeleted { get; set; }
    
    public int? DeleterId { get; set; }
    
    public User? Deleter { get; set; }
    
    public DateTimeOffset? DeletionTime { get; set; }

    public BaseFullAuditedEntity() : base()
    {
        IsDeleted = false;
    }
}