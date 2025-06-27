using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Orders.Models;

public sealed record OrderHistoryDto : BaseDto
{
    public int OrderId { get; init; }
    public string FromStatus { get; init; } = string.Empty;
    public string ToStatus { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public int? TriggeredByUserId { get; init; }
    public string? TriggeredByUserName { get; init; }
    public string? ExternalReference { get; init; }
    public DateTimeOffset Timestamp { get; init; }
}
