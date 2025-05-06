using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Branches.UpdateBranch;

public record UpdateBranchCommand : IRequest<Result<BranchDetail>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }

    // Address info
    public int AddressId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; } = string.Empty;
    public int DistrictId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public string CommuneCode { get; set; } = string.Empty;
    public string CommuneName { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public AddressType AddressType { get; set; } = AddressType.Home;
}

public class UpdateBranchCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<UpdateBranchCommand, Result<BranchDetail>>
{
    public async Task<Result<BranchDetail>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await dbContext.Branches
            .Include(b => b.Address)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (branch == null)
        {
            return Result.Failure<BranchDetail>(BranchErrors.NotFound);
        }

        // Check for duplicate branch name (excluding current branch)
        if (await dbContext.Branches.AnyAsync(b => b.Name == request.Name && b.Id != request.Id, cancellationToken))
        {
            return Result.Failure<BranchDetail>(BranchErrors.DuplicateBranchName(request.Name));
        }

        // If this branch is being set as default, ensure no other branch is already default
        if (request.IsDefault && !branch.IsDefault)
        {
            var existingDefault = await dbContext.Branches.FirstOrDefaultAsync(b => b.IsDefault && b.Id != request.Id, cancellationToken);
            if (existingDefault != null)
            {
                existingDefault.IsDefault = false;
                dbContext.Branches.Update(existingDefault);
            }
        }

        // Update branch details
        branch.Name = request.Name;
        branch.Description = request.Description;
        branch.Email = request.Email;
        branch.IsDefault = request.IsDefault;
        branch.IsDeleted = request.IsDeleted;

        // Update address
        if (branch.Address != null)
        {
            branch.Address.PhoneNumber = request.PhoneNumber;
            branch.Address.ProvinceId = request.ProvinceId;
            branch.Address.ProvinceName = request.ProvinceName;
            branch.Address.DistrictId = request.DistrictId;
            branch.Address.DistrictName = request.DistrictName;
            branch.Address.CommuneCode = request.CommuneCode;
            branch.Address.CommuneName = request.CommuneName;
            branch.Address.DetailAddress = request.DetailAddress;
            branch.Address.Type = request.AddressType;

            dbContext.BranchAddresses.Update(branch.Address);
        }

        dbContext.Branches.Update(branch);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Return updated branch details
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
                FormattedAddress = $"{branch.Address.DetailAddress}, {branch.Address.CommuneName}, {branch.Address.DistrictName}, {branch.Address.ProvinceName}"
            },
            CreationTime = branch.CreationTime,
            CreatorId = branch.CreatorId,
            LastModificationTime = branch.LastModificationTime,
            LastModifierId = branch.LastModifierId
        };

        return Result.Success(branchDetail);
    }
}