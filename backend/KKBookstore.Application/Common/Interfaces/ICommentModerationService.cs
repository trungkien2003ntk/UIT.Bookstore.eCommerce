using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Common.Interfaces;

public interface ICommentModerationService
{
    /// <summary>
    /// Evaluates a comment using AI to determine if it violates content policies
    /// </summary>
    /// <param name="comment">The comment text to evaluate</param>
    /// <param name="language">Language of the comment (e.g., "vi", "en")</param>
    /// <returns>Moderation result with badness score (1-100) and explanation</returns>
    Task<CommentModerationResult> EvaluateCommentAsync(string comment, string language = "vi");
}

public class CommentModerationResult
{
    public bool Success { get; set; }
    public int BadnessScore { get; set; } // 1-100 scale
    public string Explanation { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // e.g., "Inappropriate Language", "Spam", etc.
    public bool IsViolation { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
