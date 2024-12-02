namespace KKBookstore.Application.Common.Interfaces;

public interface IAuditedDto
{
    DateTimeOffset? CreationTime { get; set; }

    int? CreatorId { get; set; }

    int? LastModifierId { get; set; }

    DateTimeOffset? LastModificationTime { get; set; }
}