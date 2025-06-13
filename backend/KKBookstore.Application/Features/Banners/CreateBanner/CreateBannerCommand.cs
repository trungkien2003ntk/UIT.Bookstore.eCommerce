using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Banners.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.CreateBanner;

public record CreateBannerCommand : IRequest<Result<BannerDto>>
{
    public string Title { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string? TargetUrl { get; init; }
    public int? ProductTypeId { get; init; }
    public bool IsActive { get; init; } = true;
}

public class CreateBannerCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<CreateBannerCommand, Result<BannerDto>>
{
    public async Task<Result<BannerDto>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
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

        // Create the banner using domain factory method
        var createResult = Banner.Create(
            request.Title,
            request.ImageUrl,
            request.TargetUrl,
            request.ProductTypeId,
            request.IsActive
        );

        if (createResult.IsFailure)
        {
            return Result.Failure<BannerDto>(createResult.Error);
        }

        var banner = createResult.Value;
        dbContext.Banners.Add(banner);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Load the created banner with related data
        var createdBanner = await dbContext.Banners
            .Include(b => b.ProductType)
            .FirstAsync(b => b.Id == banner.Id, cancellationToken);

        var result = new BannerDto
        {
            Id = createdBanner.Id,
            Title = createdBanner.Title,
            ImageUrl = createdBanner.ImageUrl,
            TargetUrl = createdBanner.TargetUrl,
            ProductTypeId = createdBanner.ProductTypeId,
            ProductTypeName = createdBanner.ProductType?.DisplayName,
            IsActive = createdBanner.IsActive,
            CreationTime = createdBanner.CreationTime,
            CreatorId = createdBanner.CreatorId,
            LastModificationTime = createdBanner.LastModificationTime,
            LastModifierId = createdBanner.LastModifierId
        };

        return Result.Success(result);
    }
}
