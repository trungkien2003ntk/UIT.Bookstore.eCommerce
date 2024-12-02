using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Application.Common.Models;

public record BaseFullAuditedDto : BaseAuditedDto, IFullAuditedDto
{
    public DateTimeOffset? DeletionTime { get; set; }
    public int? DeleterId { get; set; }
}