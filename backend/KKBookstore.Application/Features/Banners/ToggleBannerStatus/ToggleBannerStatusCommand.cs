using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.ToggleBannerStatus;

public record ToggleBannerStatusCommand(int Id) : IRequest<Result>;

public class ToggleBannerStatusCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<ToggleBannerStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleBannerStatusCommand request, CancellationToken cancellationToken)
    {
        var banner = await dbContext.Banners
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (banner == null)
        {
            return Result.Failure(BannerErrors.NotFound);
        }

        // Use domain method to toggle status
        var toggleResult = banner.ToggleStatus();
        if (toggleResult.IsFailure)
        {
            return toggleResult;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
