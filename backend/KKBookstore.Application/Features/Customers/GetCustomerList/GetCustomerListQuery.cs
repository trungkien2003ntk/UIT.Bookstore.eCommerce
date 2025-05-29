using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;
using KKBookstore.Extensions;
using KKBookstore.Features.Customers.Models;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.GetCustomerList;

public record GetCustomerListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<CustomerSummary>>>
{
    public bool? IsDeleted { get; set; }
    public bool? IsActive { get; set; }
    public string? SearchQuery { get; set; }
    public int? CustomerTypeId { get; set; }
    public UserStatus? Status { get; set; }
}

public class GetCustomerListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerListQuery, Result<PagedResult<CustomerSummary>>>
{
    public async Task<Result<PagedResult<CustomerSummary>>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Customer> query = dbContext.Users
            .OfType<Customer>()
            .AsNoTracking()
            .Include(c => c.CustomerType);

        // Apply filters
        var searchQuery = request.SearchQuery?.ToLower().Trim();
        query = query
            .WhereIf(request.IsDeleted.HasValue, c => c.IsDeleted == request.IsDeleted!.Value)
            .WhereIf(request.IsActive.HasValue, c => c.IsActive == request.IsActive!.Value)
            .WhereIf(request.CustomerTypeId.HasValue, c => c.CustomerTypeId == request.CustomerTypeId!.Value)
            .WhereIf(request.Status.HasValue, c => c.Status == request.Status!.Value)
            .ApplyFullTextSearch(
                request.SearchQuery,
                FullTextSearchMode.All,
                fullTextFields: [c => c.FullName],
                likeFields: [c => c.Email, c => c.PhoneNumber]);

        // Apply sorting
        var validSortProperties = new List<string>
        {
            nameof(Customer.Id),
            nameof(Customer.FirstName),
            nameof(Customer.LastName),
            nameof(Customer.FullName),
            nameof(Customer.Email),
            nameof(Customer.PhoneNumber),
            nameof(Customer.IsActive),
            nameof(Customer.CreationTime)
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
            return Result.Failure<PagedResult<CustomerSummary>>(sortAndPagingResult.Error);
        }

        var paginatedCustomers = sortAndPagingResult.Value;

        if (paginatedCustomers.Items.Count == 0)
        {
            return Result.Failure<PagedResult<CustomerSummary>>(CustomerErrors.NotFound);
        }

        var mappedPaginatedCustomers = MapToCustomerSummaryResult(paginatedCustomers);

        return Result.Success(mappedPaginatedCustomers);
    }

    private PagedResult<CustomerSummary> MapToCustomerSummaryResult(PagedResult<Customer> paginatedCustomers)
    {
        return new PagedResult<CustomerSummary>(
            paginatedCustomers.Items.Select(c => new CustomerSummary
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber ?? string.Empty,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted,
                CustomerTypeId = c.CustomerTypeId,
                CustomerTypeName = c.CustomerType?.Name,
                CreationTime = c.CreationTime
            }).ToList(),
            paginatedCustomers.TotalCount,
            paginatedCustomers.PageSize,
            paginatedCustomers.PageNumber
        );
    }
}