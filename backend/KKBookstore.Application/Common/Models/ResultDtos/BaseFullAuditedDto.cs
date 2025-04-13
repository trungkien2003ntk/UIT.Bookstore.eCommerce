using KKBookstore.Common.Interfaces;

namespace KKBookstore.Common.Models.ResultDtos;

public record BaseFullAuditedDto : BaseAuditedDto, IFullAuditedDto
{
    public DateTimeOffset? DeletionTime { get; set; }
    public int? DeleterId { get; set; }
}