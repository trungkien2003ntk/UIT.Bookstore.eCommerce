using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Common.Interfaces;

public interface IFullAuditedDto : IAuditedDto
{
    DateTimeOffset? DeletionTime { get; set; }

    int? DeleterId { get; set; }
}