using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;
using KKBookstore.Extensions;
using KKBookstore.Features.Customers.Models;
using KKBookstore.Models;
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
        if (request.IsDeleted.HasValue)
        {
            query = query.Where(c => c.IsDeleted == request.IsDeleted.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        if (request.CustomerTypeId.HasValue)
        {
            query = query.Where(c => c.CustomerTypeId == request.CustomerTypeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.ToLower().Trim();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(searchQuery) ||
                c.LastName.ToLower().Contains(searchQuery) ||
                c.Email.ToLower().Contains(searchQuery) ||
                c.PhoneNumber.Contains(searchQuery));
        }

        // Apply sorting
        var validSortProperties = new List<string>
        {
            nameof(Customer.Id),
            nameof(Customer.FirstName),
            nameof(Customer.LastName),
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