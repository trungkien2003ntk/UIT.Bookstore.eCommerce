using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
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
    public CreateProductRatingCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ProductRatingDto>> Handle(CreateProductRatingCommand request, CancellationToken cancellationToken)
    {
        var productVariant = await _dbContext.ProductVariants
            .Include(v => v.ProductVariantOptionValues)!
                .ThenInclude(x => x.OptionValue)
            .FirstOrDefaultAsync(v => v.Id == request.ProductVariantId, cancellationToken);
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
            UserName = "N/A",
            UserAvatarUrl = null,
            ProductVariantName = MappingHelpers.GetProductVariantOptionValuesString(productVariant),
            LikesCount = 0,
            ReportsCount = 0,
            Response = null,
            Status = rating.Status.ToString()
        };
        return Result.Success(ratingDto);
    }
}