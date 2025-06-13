using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.DeleteBanner;

public record DeleteBannerCommand(int Id) : IRequest<Result>;

public class DeleteBannerCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteBannerCommand, Result>
{
    public async Task<Result> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await dbContext.Banners
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (banner == null)
        {
            return Result.Failure(BannerErrors.NotFound);
        }

        dbContext.Banners.Remove(banner);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
