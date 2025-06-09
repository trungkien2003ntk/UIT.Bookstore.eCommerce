using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Constants;
using KKBookstore.Emailing;
using KKBookstore.Emailing.TemplateModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Features.Admin.Services;

public interface IModerationNotificationService
{
    Task NotifyAdminOfAutoHiddenRatingAsync(int ratingId, string comment, int aiScore);
    Task NotifyUserOfHiddenRatingAsync(string userEmail, int ratingId, string reason);
    Task NotifyUserOfRestoredRatingAsync(string userEmail, int ratingId);
}

public class ModerationNotificationService : IModerationNotificationService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ModerationNotificationService> _logger;
    private readonly IIdentityService _identityService;
    private readonly ModerationConfiguration _config;

    public ModerationNotificationService(
        IEmailService emailService,
        ILogger<ModerationNotificationService> logger,
        IOptions<ModerationConfiguration> config,
        IIdentityService identityService)
    {
        _emailService = emailService;
        _logger = logger;
        _config = config.Value;
        _identityService = identityService;
    }
    public async Task NotifyAdminOfAutoHiddenRatingAsync(int ratingId, string comment, int aiScore)
    {
        var getAdminsResult = await _identityService.GetUsersInRoleAsync(AppRoles.Admin);
        if (getAdminsResult.IsFailure || getAdminsResult.Value.Count == 0)
        {
            _logger.LogWarning("No admins found to notify for auto-hidden rating {RatingId}", ratingId);
            return;
        }

        var adminEmails = getAdminsResult.Value
            .Where(u => !string.IsNullOrEmpty(u.Email))
            .Select(u => u.Email!)
            .ToList();

        if (adminEmails is null || adminEmails.Count == 0)
        {
            _logger.LogWarning("No admin emails configured for moderation notifications");
            return;
        }

        var emailModel = new AdminAutoHiddenRatingEmailModel(ratingId, comment, aiScore);

        foreach (var adminEmail in adminEmails)
        {
            try
            {
                await _emailService.SendAsync(adminEmail, emailModel.Subject, emailModel);
                _logger.LogInformation("Auto-hidden rating notification sent to admin {AdminEmail} for rating {RatingId}",
                    adminEmail, ratingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send auto-hidden rating notification to admin {AdminEmail} for rating {RatingId}",
                    adminEmail, ratingId);
            }
        }
    }
    public async Task NotifyUserOfHiddenRatingAsync(string userEmail, int ratingId, string reason)
    {
        var emailModel = new UserHiddenRatingEmailModel(ratingId, reason);

        try
        {
            await _emailService.SendAsync(userEmail, emailModel.Subject, emailModel);
            _logger.LogInformation("Hidden rating notification sent to user {UserEmail} for rating {RatingId}",
                userEmail, ratingId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send hidden rating notification to user {UserEmail} for rating {RatingId}",
                userEmail, ratingId);
        }
    }
    public async Task NotifyUserOfRestoredRatingAsync(string userEmail, int ratingId)
    {
        var emailModel = new UserRestoredRatingEmailModel(ratingId);

        try
        {
            await _emailService.SendAsync(userEmail, emailModel.Subject, emailModel);
            _logger.LogInformation("Restored rating notification sent to user {UserEmail} for rating {RatingId}",
                userEmail, ratingId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send restored rating notification to user {UserEmail} for rating {RatingId}",
                userEmail, ratingId);
        }
    }
}
