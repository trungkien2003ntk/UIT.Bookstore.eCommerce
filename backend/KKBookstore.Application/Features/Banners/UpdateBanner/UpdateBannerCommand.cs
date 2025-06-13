using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Banners.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.UpdateBanner;

public record UpdateBannerCommand : IRequest<Result<BannerDto>>
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string? TargetUrl { get; init; }
    public int? ProductTypeId { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateBannerCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<UpdateBannerCommand, Result<BannerDto>>
{
    public async Task<Result<BannerDto>> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await dbContext.Banners
            .Include(b => b.ProductType)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (banner == null)
        {
            return Result.Failure<BannerDto>(BannerErrors.NotFound);
        }

        // Validate ProductType exists if specified
        if (request.ProductTypeId.HasValue)
        {
            var productTypeExists = await dbContext.ProductTypes
                .AnyAsync(pt => pt.Id == request.ProductTypeId.Value, cancellationToken);

            if (!productTypeExists)
            {
                return Result.Failure<BannerDto>(
                    Error.NotFound("ProductType.NotFound", "The specified product type does not exist"));
            }
        }

        // Validate business rules using domain methods
        var titleValidation = banner.UpdateTitle(request.Title);
        if (titleValidation.IsFailure)
        {
            return Result.Failure<BannerDto>(titleValidation.Error);
        }

        var imageUrlValidation = banner.UpdateImageUrl(request.ImageUrl);
        if (imageUrlValidation.IsFailure)
        {
            return Result.Failure<BannerDto>(imageUrlValidation.Error);
        }

        var targetUrlValidation = banner.UpdateTargetUrl(request.TargetUrl);
        if (targetUrlValidation.IsFailure)
        {
            return Result.Failure<BannerDto>(targetUrlValidation.Error);
        }

        // Update properties
        banner.ProductTypeId = request.ProductTypeId;
        banner.IsActive = request.IsActive;

        await dbContext.SaveChangesAsync(cancellationToken);

        // Reload the banner with updated data
        var updatedBanner = await dbContext.Banners
            .Include(b => b.ProductType)
            .FirstAsync(b => b.Id == banner.Id, cancellationToken);

        var result = new BannerDto
        {
            Id = updatedBanner.Id,
            Title = updatedBanner.Title,
            ImageUrl = updatedBanner.ImageUrl,
            TargetUrl = updatedBanner.TargetUrl,
            ProductTypeId = updatedBanner.ProductTypeId,
            ProductTypeName = updatedBanner.ProductType?.DisplayName,
            IsActive = updatedBanner.IsActive,
            CreationTime = updatedBanner.CreationTime,
            CreatorId = updatedBanner.CreatorId,
            LastModificationTime = updatedBanner.LastModificationTime,
            LastModifierId = updatedBanner.LastModifierId
        };

        return Result.Success(result);
    }
}
