using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Banners.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.GetBannerDetail;

public record GetBannerDetailQuery(int Id) : IRequest<Result<BannerDto>>;

public class GetBannerDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetBannerDetailQuery, Result<BannerDto>>
{
    public async Task<Result<BannerDto>> Handle(GetBannerDetailQuery request, CancellationToken cancellationToken)
    {
        var banner = await dbContext.Banners
            .Include(b => b.ProductType)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (banner == null)
        {
            return Result.Failure<BannerDto>(BannerErrors.NotFound);
        }

        var result = new BannerDto
        {
            Id = banner.Id,
            Title = banner.Title,
            ImageUrl = banner.ImageUrl,
            TargetUrl = banner.TargetUrl,
            ProductTypeId = banner.ProductTypeId,
            ProductTypeName = banner.ProductType?.DisplayName,
            IsActive = banner.IsActive,
            CreationTime = banner.CreationTime,
            CreatorId = banner.CreatorId,
            LastModificationTime = banner.LastModificationTime,
            LastModifierId = banner.LastModifierId
        };

        return Result.Success(result);
    }
}
