using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.CreateProductRating;

public record CreateProductRatingCommand(
    int ProductVariantId,
    int CustomerId,
    string Comment,
    int RatingValue,
    List<string> ImageUrls
) : IRequest<Result<ProductRatingDto>>
{
}

public class CreateProductRatingCommandHandler : IRequestHandler<CreateProductRatingCommand, Result<ProductRatingDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;
    public CreateProductRatingCommandHandler(IApplicationDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<ProductRatingDto>> Handle(CreateProductRatingCommand request, CancellationToken cancellationToken)
    {
        var productVariant = await _dbContext.ProductVariants
            .Include(v => v.ProductVariantOptionValues)!
                .ThenInclude(x => x.OptionValue)
            .FirstOrDefaultAsync(v => v.Id == request.ProductVariantId, cancellationToken);

        var currentUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

        if (currentUser == null)
        {
            return Result.Failure<ProductRatingDto>(UserErrors.NotFound);
        }

        if (productVariant == null)
        {
            return Result.Failure<ProductRatingDto>(ProductErrors.NotFound);
        }

        var createRatingResult = Rating.Create(
            request.Comment,
            request.RatingValue,
            request.CustomerId,
            productVariant,
            request.ImageUrls
        );

        if (createRatingResult.IsFailure)
        {
            return Result.Failure<ProductRatingDto>(createRatingResult.Error);
        }
        var rating = createRatingResult.Value;

        await _dbContext.Ratings.AddAsync(rating, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var ratingDto = new ProductRatingDto
        {
            Id = rating.Id,
            Comment = rating.Comment!,
            RatingValue = rating.RatingValue,
            CustomerId = rating.CustomerId,
            UserName = currentUser.UserName ?? "Anonymous User",
            FullName = currentUser.FullName ?? "Anonymous User",
            UserAvatarUrl = null,
            ProductVariantName = MappingHelpers.GetProductVariantOptionValuesString(productVariant),
            LikesCount = 0,
            ReportsCount = 0,
            Response = null,
            Status = rating.Status
        };
        return Result.Success(ratingDto);
    }
}