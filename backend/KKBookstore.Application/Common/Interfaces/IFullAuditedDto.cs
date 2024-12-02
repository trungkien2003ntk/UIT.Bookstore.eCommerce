namespace KKBookstore.Application.Common.Interfaces;

public interface IFullAuditedDto : IAuditedDto
{
    DateTimeOffset? DeletionTime { get; set; }

    int? DeleterId { get; set; }
}