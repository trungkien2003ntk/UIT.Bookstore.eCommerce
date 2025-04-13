using KKBookstore.Common.Interfaces;

namespace KKBookstore.Common.Models.ResultDtos;

public abstract record BaseAuditedDto : BaseDto, IAuditedDto
{
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public int? LastModifierId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
}