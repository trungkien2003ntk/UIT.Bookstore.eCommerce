using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetProductRatingList;

public record GetProductRatingListQuery : IRequest<Result<ProductRatingSummary>>, IPaginatedQuery
{
    public int ProductId { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public List<string> Statuses { get; init; } = [];
}

public class GetProductRatingListQueryHandler(
        IApplicationDbContext dbContext,
        IMapper mapper
    ) : IRequestHandler<GetProductRatingListQuery, Result<ProductRatingSummary>>
{
    public async Task<Result<ProductRatingSummary>> Handle(GetProductRatingListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(s => s.ProductVariantOptionValues)
                    .ThenInclude(sov => sov.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Likes)
            .Where(x => x.ProductVariant.ProductId == request.ProductId)
            .AsQueryable();

        var queryResult = ApplyStatusFilter(query, request);

        if (queryResult.IsFailure)
        {
            return Result.Failure<ProductRatingSummary>(queryResult.Error);
        }

        query = queryResult.Value;

        var allRatings = await query.ToListAsync(cancellationToken);

        PaginatedResult<Rating> paginatedRatings;

        paginatedRatings = await query.SortAndPaginateAsync(
            "CreationTime",
            "desc",
            ["CreationTime"],
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (paginatedRatings.Items.Count == 0)
        {
            return Result.Failure<ProductRatingSummary>(ProductErrors.RatingNotFound);
        }

        var result = mapper.Map<ProductRatingSummary>((paginatedRatings, allRatings));

        return Result.Success(result);
    }

    private Result<IQueryable<Rating>> ApplyStatusFilter(IQueryable<Rating> query, GetProductRatingListQuery request)
    {
        // check if the statuses in the request are valid
        if (request.Statuses.Exists(x => !Enum.TryParse<RatingStatus>(x, out _)))
        {
            return Result.Failure<IQueryable<Rating>>(
                ProductErrors.InvalidAttributeValue(
                    nameof(Rating.Status),
                    Enum.GetNames(typeof(RatingStatus))
                ));
        }

        var statuses = request.Statuses.Select(x => Enum.Parse<RatingStatus>(x)).ToList();

        return Result.Success(query.Where(x => statuses.Contains(x.Status)));
    }
}