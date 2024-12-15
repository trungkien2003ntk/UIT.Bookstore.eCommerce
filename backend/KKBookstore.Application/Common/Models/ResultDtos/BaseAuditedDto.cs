using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Application.Common.Models.ResultDtos;

public abstract record BaseAuditedDto : BaseDto, IAuditedDto
{
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public int? LastModifierId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
}