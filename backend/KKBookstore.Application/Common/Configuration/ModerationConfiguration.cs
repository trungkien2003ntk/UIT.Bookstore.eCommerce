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

    /// <summary>
    /// Settings for different moderation levels
    /// </summary>
    public ModerationLevelSettings Relaxed { get; set; } = new() { AutoHideThreshold = 85, AiTemperature = 0.4 };
    public ModerationLevelSettings Medium { get; set; } = new() { AutoHideThreshold = 70, AiTemperature = 0.3 };
    public ModerationLevelSettings Strict { get; set; } = new() { AutoHideThreshold = 55, AiTemperature = 0.2 };

    /// <summary>
    /// Get current level settings
    /// </summary>
    public ModerationLevelSettings GetCurrentLevelSettings(string currentLevel)
    {
        Enum.TryParse<ModerationLevel>(currentLevel, out var level);
        return level switch
        {
            ModerationLevel.Relaxed => Relaxed,
            ModerationLevel.Medium => Medium,
            ModerationLevel.Strict => Strict,
            _ => Medium
        };
    }
}

public class ModerationLevelSettings
{
    public int AutoHideThreshold { get; set; } = 70;
    public double AiTemperature { get; set; } = 0.3;
    public int MaxTokens { get; set; } = 200;
}

public enum ModerationLevel
{
    Relaxed = 1,
    Medium = 2,
    Strict = 3
}
