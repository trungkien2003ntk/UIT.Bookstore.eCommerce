using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Branches.GetBranchList;

public record GetBranchListQuery()
    : PagedAndSortedResultRequest, IRequest<Result<PagedResult<BranchSummary>>>
{
    public bool? IsDeleted { get; set; }
    public bool? IsDefault { get; set; }
    public bool? IsActive { get; set; }
    public string? SearchQuery { get; set; }
}

public class GetBranchListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetBranchListQuery, Result<PagedResult<BranchSummary>>>
{
    public async Task<Result<PagedResult<BranchSummary>>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Branch> query = dbContext.Branches
            .AsNoTracking()
            .Include(b => b.Address);

        query = query.WhereIf(request.IsDeleted.HasValue, b => b.IsDeleted == request.IsDeleted!.Value)
            .WhereIf(request.IsActive.HasValue, b => b.IsDeleted == !request.IsActive!.Value)
            .WhereIf(request.IsDefault.HasValue, b => b.IsDefault == request.IsDefault!.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.ToLower().Trim();
            query = query.Where(b =>
                b.Name.ToLower().Contains(searchQuery) ||
                b.Description.ToLower().Contains(searchQuery) ||
                b.Email.ToLower().Contains(searchQuery));
        }

        // Apply sorting
        var validSortProperties = new List<string>
        {
            nameof(Branch.Id),
            nameof(Branch.Name),
            nameof(Branch.Email),
            nameof(Branch.IsDefault),
            nameof(Branch.CreationTime)
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
            return Result.Failure<PagedResult<BranchSummary>>(sortAndPagingResult.Error);
        }

        var paginatedBranches = sortAndPagingResult.Value;

        if (paginatedBranches.Items.Count == 0)
        {
            return Result.Failure<PagedResult<BranchSummary>>(BranchErrors.NotFound);
        }

        var mappedPaginatedBranches = MapToBranchSummaryResult(paginatedBranches);

        return Result.Success(mappedPaginatedBranches);
    }

    private PagedResult<BranchSummary> MapToBranchSummaryResult(PagedResult<Branch> paginatedBranches)
    {
        return new PagedResult<BranchSummary>(
            paginatedBranches.Items.Select(b => new BranchSummary
            {
                Id = b.Id,
                Name = b.Name,
                Email = b.Email,
                Description = b.Description,
                IsDefault = b.IsDefault,
                IsDeleted = b.IsDeleted,
                IsActive = b.IsActive,
                Address = new AddressSummary
                {
                    ProvinceId = b.Address.ProvinceId,
                    ProvinceName = b.Address.ProvinceName,
                    DistrictId = b.Address.DistrictId,
                    DistrictName = b.Address.DistrictName,
                    CommuneCode = b.Address.CommuneCode,
                    CommuneName = b.Address.CommuneName,
                    DetailAddress = b.Address.DetailAddress,
                    PhoneNumber = b.Address.PhoneNumber,
                    FormattedAddress = $"{b.Address.DetailAddress}, {b.Address.CommuneName}, {b.Address.DistrictName}, {b.Address.ProvinceName}"
                },
                CreationTime = b.CreationTime
            }).ToList(),
            paginatedBranches.TotalCount,
            paginatedBranches.PageSize,
            paginatedBranches.PageNumber
        );
    }
}