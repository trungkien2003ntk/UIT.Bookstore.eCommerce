namespace KKBookstore.Common.Configuration;

public class ModerationConfiguration
{
    public const string SectionName = "Moderation";

    /// <summary>
    /// Number of reports needed to trigger AI moderation
    /// </summary>
    public int ReportThresholdForAiModeration { get; set; } = 3;

    /// <summary>
    /// AI badness score threshold above which comments are auto-hidden (1-100)
    /// </summary>
    public int AutoHideThreshold { get; set; } = 70;

    /// <summary>
    /// Whether AI moderation is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Temperature setting for AI analysis (0.0-1.0)
    /// </summary>
    public double AiTemperature { get; set; } = 0.3;

    /// <summary>
    /// Maximum tokens for AI response
    /// </summary>
    public int MaxTokens { get; set; } = 200;

    /// <summary>
    /// Default language for content policy
    /// </summary>
    public string DefaultLanguage { get; set; } = "vi";
}
