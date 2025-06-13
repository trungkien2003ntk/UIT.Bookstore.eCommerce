using KKBookstore.Models;

namespace KKBookstore.Products;

public class ModerationAuditLog : BaseAuditedEntity
{
    public ModerationAuditLog()
    {
    }

    public ModerationAuditLog(
        int ratingId,
        string action,
        string details,
        int? moderatorId = null,
        int? aiScore = null
    ) : base()
    {
        RatingId = ratingId;
        Action = action;
        Details = details;
        ModeratorId = moderatorId;
        AiScore = aiScore;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public int RatingId { get; set; }
    public string Action { get; set; } = null!; // e.g., "AI_EVALUATED", "AUTO_HIDDEN", "MANUALLY_RESTORED", etc.
    public string Details { get; set; } = null!; // JSON with additional context
    public int? ModeratorId { get; set; } // Admin who took manual action
    public int? AiScore { get; set; } // AI badness score (1-100)
    public DateTimeOffset Timestamp { get; set; }

    // Navigation properties
    public Rating Rating { get; set; } = null!;
}
