using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Interfaces;

public interface IFullAuditedObject : IAuditedObject
{
    bool IsDeleted { get; set; }
    
    int? DeleterId { get; set; }
    
    User? Deleter { get; set; }
    
    DateTimeOffset? DeletionTime { get; set; }
}