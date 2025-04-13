using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Common.Models.ResultDtos;

public record BaseFullAuditedDto : BaseAuditedDto, IFullAuditedDto
{
    public DateTimeOffset? DeletionTime { get; set; }
    public int? DeleterId { get; set; }
}