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

    // Enhanced moderation properties
    public int? ModerationLevel { get; set; } // Moderation level used (1=Relaxed, 2=Medium, 3=Strict)
    public int? ThresholdUsed { get; set; } // Threshold value used for auto-hide decision

    // Navigation properties
    public Rating Rating { get; set; } = null!;
}
