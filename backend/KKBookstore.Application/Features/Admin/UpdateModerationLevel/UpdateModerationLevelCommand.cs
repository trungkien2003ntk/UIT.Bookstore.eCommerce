using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Admin.UpdateModerationLevel;

public record UpdateModerationLevelCommand(ModerationLevel Level) : IRequest<Result>;

public class UpdateModerationLevelCommandHandler : IRequestHandler<UpdateModerationLevelCommand, Result>
{
    private readonly IApplicationDbContext dbContext;
    private readonly ILogger<UpdateModerationLevelCommandHandler> _logger;

    public UpdateModerationLevelCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<UpdateModerationLevelCommandHandler> logger)
    {
        this.dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateModerationLevelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentModerationSetting = await dbContext.Settings
                .FirstOrDefaultAsync(x => x.Key == ApplicationSettingKeys.CurrentModerationLevel, cancellationToken: cancellationToken);

            if (currentModerationSetting == null)
            {
                return Result.Failure<Result>(Error.NotFound("ModerationSetting.NotFound", "Cannot find moderation setting"));
            }

            // Update the configuration in memory
            currentModerationSetting.Value = request.Level.ToString();

            _logger.LogInformation("Moderation level updated to {Level}", request.Level);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating moderation level to {Level}", request.Level);
            return Result.Failure(Error.Failure("UpdateModerationLevel.Failed", "Failed to update moderation level"));
        }
    }
}