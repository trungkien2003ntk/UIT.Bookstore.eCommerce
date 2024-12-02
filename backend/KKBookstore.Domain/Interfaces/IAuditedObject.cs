using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Interfaces;

public interface IAuditedObject
{
    DateTimeOffset? CreationTime { get; set; }
    
    int? CreatorId { get; set; }
    
    User? Creator { get; set; }
    
    int? LastModifierId { get; set; }
    
    User? LastModifier { get; set; }
    
    DateTimeOffset? LastModificationTime { get; set; }
}
