using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Branches.GetBranchDetail;

public record GetBranchDetailQuery(int Id) : IRequest<Result<BranchDetail>>;

public class GetBranchDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetBranchDetailQuery, Result<BranchDetail>>
{
    public async Task<Result<BranchDetail>> Handle(GetBranchDetailQuery request, CancellationToken cancellationToken)
    {
        var branch = await dbContext.Branches
            .AsNoTracking()
            .Include(b => b.Address)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (branch is null)
        {
            return Result.Failure<BranchDetail>(BranchErrors.NotFound);
        }

        var branchDetail = new BranchDetail
        {
            Id = branch.Id,
            Name = branch.Name,
            Description = branch.Description,
            Email = branch.Email,
            IsDefault = branch.IsDefault,
            IsDeleted = branch.IsDeleted,
            Address = new AddressDetail
            {
                Id = branch.Address.Id,
                PhoneNumber = branch.Address.PhoneNumber,
                ProvinceId = branch.Address.ProvinceId,
                ProvinceName = branch.Address.ProvinceName,
                DistrictId = branch.Address.DistrictId,
                DistrictName = branch.Address.DistrictName,
                CommuneCode = branch.Address.CommuneCode,
                CommuneName = branch.Address.CommuneName,
                DetailAddress = branch.Address.DetailAddress,
                AddressType = branch.Address.Type,
                FormattedAddress = $"{branch.Address.DetailAddress}, {branch.Address.CommuneName},  {branch.Address.DistrictName}, {branch.Address.ProvinceName}"
            },
            CreationTime = branch.CreationTime,
            CreatorId = branch.CreatorId,
            LastModificationTime = branch.LastModificationTime,
            LastModifierId = branch.LastModifierId
        };

        return Result.Success(branchDetail);
    }
}