using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Features.Admin.GetModerationSettings;

public record GetModerationSettingsQuery() : IRequest<Result<ModerationSettingsResponse>>;

public class ModerationSettingsResponse
{
    public ModerationLevel CurrentLevel { get; set; }
    public bool IsEnabled { get; set; }
    public int ReportThresholdForAiModeration { get; set; }
    public Dictionary<string, ModerationLevelSettingsDto> Levels { get; set; } = new();
}

public class ModerationLevelSettingsDto
{
    public int AutoHideThreshold { get; set; }
    public double AiTemperature { get; set; }
    public int MaxTokens { get; set; }
}

public class GetModerationSettingsQueryHandler : IRequestHandler<GetModerationSettingsQuery, Result<ModerationSettingsResponse>>
{
    private readonly ModerationConfiguration _config;
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetModerationSettingsQueryHandler> _logger;

    public GetModerationSettingsQueryHandler(
        IOptions<ModerationConfiguration> config,
        ILogger<GetModerationSettingsQueryHandler> logger,
        IApplicationDbContext context)
    {
        _config = config.Value;
        _logger = logger;
        _context = context;
    }

    public async Task<Result<ModerationSettingsResponse>> Handle(GetModerationSettingsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var currentLevel = await _context.Settings
                .FirstOrDefaultAsync(x => x.Key == ApplicationSettingKeys.CurrentModerationLevel, cancellationToken: cancellationToken);

            if (currentLevel == null)
            {
                return Result.Failure<ModerationSettingsResponse>(Error.NotFound("NotFound", "Not Found"));
            }

            var currLevelEnum = Enum.Parse<ModerationLevel>(currentLevel.Value);
            var response = new ModerationSettingsResponse
            {
                CurrentLevel = currLevelEnum,
                IsEnabled = _config.IsEnabled,
                ReportThresholdForAiModeration = _config.ReportThresholdForAiModeration,
                Levels = new Dictionary<string, ModerationLevelSettingsDto>
                {
                    ["Relaxed"] = new()
                    {
                        AutoHideThreshold = _config.Relaxed.AutoHideThreshold,
                        AiTemperature = _config.Relaxed.AiTemperature,
                        MaxTokens = _config.Relaxed.MaxTokens
                    },
                    ["Medium"] = new()
                    {
                        AutoHideThreshold = _config.Medium.AutoHideThreshold,
                        AiTemperature = _config.Medium.AiTemperature,
                        MaxTokens = _config.Medium.MaxTokens
                    },
                    ["Strict"] = new()
                    {
                        AutoHideThreshold = _config.Strict.AutoHideThreshold,
                        AiTemperature = _config.Strict.AiTemperature,
                        MaxTokens = _config.Strict.MaxTokens
                    }
                }
            };

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting moderation settings");
            return Result.Failure<ModerationSettingsResponse>(Error.Failure("GetModerationSettings.Failed", "Failed to get moderation settings"));
        }
    }
}