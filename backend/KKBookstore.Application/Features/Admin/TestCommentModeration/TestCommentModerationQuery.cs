using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Features.Admin.TestCommentModeration;

public record TestCommentModerationQuery(string Comment, int? ProductId = null, string Language = "vi")
    : IRequest<Result<TestCommentModerationResponse>>;

public class TestCommentModerationResponse
{
    public string Comment { get; set; } = string.Empty;
    public int? ProductId { get; set; }
    public string Language { get; set; } = string.Empty;
    public ModerationLevel CurrentLevel { get; set; }
    public int CurrentThreshold { get; set; }
    public bool WillBeHidden { get; set; }
    public CommentModerationResult ModerationResult { get; set; } = new();
    public string TestResult { get; set; } = string.Empty;
}

public class TestCommentModerationQueryHandler : IRequestHandler<TestCommentModerationQuery, Result<TestCommentModerationResponse>>
{
    private readonly IApplicationDbContext context;
    private readonly ICommentModerationService _moderationService;
    private readonly ModerationConfiguration _config;
    private readonly ILogger<TestCommentModerationQueryHandler> _logger;

    public TestCommentModerationQueryHandler(
        IApplicationDbContext context,
        ICommentModerationService moderationService,
        IOptions<ModerationConfiguration> config,
        ILogger<TestCommentModerationQueryHandler> logger)
    {
        this.context = context;
        _moderationService = moderationService;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<Result<TestCommentModerationResponse>> Handle(TestCommentModerationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Comment))
            {
                return Result.Failure<TestCommentModerationResponse>(
                    Error.Validation("TestCommentModeration.EmptyComment", "Comment cannot be empty"));
            }

            _logger.LogInformation("Testing comment moderation for comment: {Comment}", request.Comment);

            // Get current moderation level settings
            var currentLevel = await context.Settings
                .FirstOrDefaultAsync(x => x.Key == ApplicationSettingKeys.CurrentModerationLevel, cancellationToken: cancellationToken);

            if (currentLevel == null)
            {
                return Result.Failure<TestCommentModerationResponse>(Error.NotFound("NotFound", "Not Found"));
            }

            var currLevelEnum = Enum.Parse<ModerationLevel>(currentLevel.Value);

            var currentLevelSettings = _config.GetCurrentLevelSettings(currentLevel.Value);
            var currentThreshold = currentLevelSettings.AutoHideThreshold;

            // Evaluate the comment using the moderation service
            CommentModerationResult moderationResult;
            if (request.ProductId.HasValue)
            {
                moderationResult = await _moderationService.EvaluateCommentWithContextAsync(
                    request.Comment, request.ProductId.Value, request.Language);
            }
            else
            {
                moderationResult = await _moderationService.EvaluateCommentAsync(
                    request.Comment, request.Language);
            }

            // Determine if the comment would be hidden based on current settings
            bool willBeHidden = false;
            string testResult = "Comment would be approved";

            if (_config.IsEnabled && moderationResult.Success)
            {
                willBeHidden = moderationResult.BadnessScore >= currentThreshold;
                testResult = willBeHidden
                    ? $"Comment would be auto-hidden (Score: {moderationResult.BadnessScore} >= Threshold: {currentThreshold})"
                    : $"Comment would be approved (Score: {moderationResult.BadnessScore} < Threshold: {currentThreshold})";
            }
            else if (!_config.IsEnabled)
            {
                testResult = "Moderation is disabled - comment would be approved";
            }
            else if (!moderationResult.Success)
            {
                testResult = $"Moderation failed - comment would be approved by default. Error: {moderationResult.ErrorMessage}";
            }

            var response = new TestCommentModerationResponse
            {
                Comment = request.Comment,
                ProductId = request.ProductId,
                Language = request.Language,
                CurrentLevel = currLevelEnum,
                CurrentThreshold = currentThreshold,
                WillBeHidden = willBeHidden,
                ModerationResult = moderationResult,
                TestResult = testResult
            };

            _logger.LogInformation("Comment moderation test completed. Will be hidden: {WillBeHidden}, Score: {Score}",
                willBeHidden, moderationResult.BadnessScore);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error testing comment moderation for comment: {Comment}", request.Comment);
            return Result.Failure<TestCommentModerationResponse>(
                Error.Failure("TestCommentModeration.Failed", "Failed to test comment moderation"));
        }
    }
}