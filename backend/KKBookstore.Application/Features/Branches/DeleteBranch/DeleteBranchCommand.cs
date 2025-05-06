using KKBookstore.Branches;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Branches.DeleteBranch;

public record DeleteBranchCommand(int Id) : IRequest<Result>;

public class DeleteBranchCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteBranchCommand, Result>
{
    public async Task<Result> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await dbContext.Branches
            .Include(b => b.Address)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (branch == null)
        {
            return Result.Failure(BranchErrors.NotFound);
        }

        if (branch.IsDefault)
        {
            return Result.Failure(BranchErrors.CannotDeleteDefault);
        }

        // Delete the branch address
        if (branch.Address != null)
        {
            dbContext.BranchAddresses.Remove(branch.Address);
        }

        // Delete the branch
        dbContext.Branches.Remove(branch);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}