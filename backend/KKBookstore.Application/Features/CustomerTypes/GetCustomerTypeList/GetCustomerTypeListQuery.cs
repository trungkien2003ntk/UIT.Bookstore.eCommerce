using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;
using KKBookstore.Extensions;
using KKBookstore.Features.CustomerTypes.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.CustomerTypes.GetCustomerTypeList;

public record GetCustomerTypeListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<CustomerTypeSummary>>>
{
    public CustomerTier? Tier { get; set; }
    public string? SearchQuery { get; set; }
    public decimal? MinSpendingFrom { get; set; }
    public decimal? MinSpendingTo { get; set; }
}

public class GetCustomerTypeListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerTypeListQuery, Result<PagedResult<CustomerTypeSummary>>>
{
    public async Task<Result<PagedResult<CustomerTypeSummary>>> Handle(GetCustomerTypeListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CustomerType> query = dbContext.CustomerTypes
            .AsNoTracking();

        // Apply filters
        if (request.Tier.HasValue)
        {
            query = query.Where(ct => ct.Tier == request.Tier.Value);
        }
        if (request.MinSpendingFrom.HasValue)
        {
            query = query.Where(ct => ct.MinSpending >= (double)request.MinSpendingFrom.Value);
        }

        if (request.MinSpendingTo.HasValue)
        {
            query = query.Where(ct => ct.MinSpending <= (double)request.MinSpendingTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.ToLower().Trim();
            query = query.Where(ct =>
                ct.Name.ToLower().Contains(searchQuery));
        }

        // Apply sorting
        var validSortProperties = new List<string>
        {
            nameof(CustomerType.Id),
            nameof(CustomerType.Name),
            nameof(CustomerType.Tier),
            nameof(CustomerType.MinSpending),
            nameof(CustomerType.CreationTime)
        };

        var sortAndPagingResult = await query.SortAndPaginateWithResultAsync(
            request.SortBy,
            request.SortDirection,
            validSortProperties,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (sortAndPagingResult.IsFailure)
        {
            return Result.Failure<PagedResult<CustomerTypeSummary>>(sortAndPagingResult.Error);
        }

        var paginatedCustomerTypes = sortAndPagingResult.Value;

        if (paginatedCustomerTypes.Items.Count == 0)
        {
            return Result.Failure<PagedResult<CustomerTypeSummary>>(CustomerTypeErrors.NotFound);
        }

        var mappedPaginatedCustomerTypes = MapToCustomerTypeSummaryResult(paginatedCustomerTypes);

        return Result.Success(mappedPaginatedCustomerTypes);
    }

    private PagedResult<CustomerTypeSummary> MapToCustomerTypeSummaryResult(PagedResult<CustomerType> paginatedCustomerTypes)
    {
        return new PagedResult<CustomerTypeSummary>(
            paginatedCustomerTypes.Items.Select(ct => new CustomerTypeSummary
            {
                Id = ct.Id,
                Name = ct.Name,
                Tier = ct.Tier,
                MinSpending = (decimal)ct.MinSpending,
                CreationTime = ct.CreationTime,
                CreatorId = ct.CreatorId,
                LastModificationTime = ct.LastModificationTime,
                LastModifierId = ct.LastModifierId,
                DeletionTime = ct.DeletionTime,
                DeleterId = ct.DeleterId
            }).ToList(),
            paginatedCustomerTypes.TotalCount,
            paginatedCustomerTypes.PageSize,
            paginatedCustomerTypes.PageNumber
        );
    }
}
