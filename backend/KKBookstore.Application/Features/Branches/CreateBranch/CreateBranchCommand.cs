using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Branches.CreateBranch;

public record CreateBranchCommand : IRequest<Result<BranchDetail>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDefault { get; set; }

    // Address info
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

public class CreateBranchCommandHandler(
    IApplicationDbContext dbContext,
    ICurrentUser currentUser
) : IRequestHandler<CreateBranchCommand, Result<BranchDetail>>
{
    public async Task<Result<BranchDetail>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        // Check for duplicate branch name
        if (await dbContext.Branches.AnyAsync(b => b.Name == request.Name, cancellationToken))
        {
            return Result.Failure<BranchDetail>(BranchErrors.DuplicateBranchName(request.Name));
        }

        // If this branch is set as default, ensure no other branch is already default
        if (request.IsDefault && await dbContext.Branches.AnyAsync(b => b.IsDefault, cancellationToken))
        {
            return Result.Failure<BranchDetail>(BranchErrors.DefaultBranchExists);
        }

        // Create the branch
        var branch = new Branch
        {
            Name = request.Name,
            Description = request.Description,
            Email = request.Email,
            IsDefault = request.IsDefault,
            IsDeleted = true
        };

        // Add the branch first to get its ID
        dbContext.Branches.Add(branch);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Create address
        var address = new BranchAddress(
            request.PhoneNumber,
            request.ProvinceId,
            request.ProvinceName,
            request.DistrictId,
            request.DistrictName,
            request.CommuneCode,
            request.CommuneName,
            request.DetailAddress,
            true, // isDefault
            request.AddressType,
            branch.Id
        );

        // Set the address ID on the branch
        dbContext.BranchAddresses.Add(address);
        await dbContext.SaveChangesAsync(cancellationToken);

        branch.AddressId = address.Id;
        await dbContext.SaveChangesAsync(cancellationToken);

        // Return the branch details
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
                Id = address.Id,
                PhoneNumber = address.PhoneNumber,
                ProvinceId = address.ProvinceId,
                ProvinceName = address.ProvinceName,
                DistrictId = address.DistrictId,
                DistrictName = address.DistrictName,
                CommuneCode = address.CommuneCode,
                CommuneName = address.CommuneName,
                DetailAddress = address.DetailAddress,
                AddressType = address.Type,
                FormattedAddress = $"{address.DetailAddress}, {address.CommuneName}, {address.DistrictName}, {address.ProvinceName}"
            },
            CreationTime = branch.CreationTime,
            CreatorId = branch.CreatorId,
            LastModificationTime = branch.LastModificationTime,
            LastModifierId = branch.LastModifierId
        };

        return Result.Success(branchDetail);
    }
}