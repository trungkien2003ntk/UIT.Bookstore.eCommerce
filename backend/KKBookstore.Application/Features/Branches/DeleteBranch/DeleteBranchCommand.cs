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
            .IgnoreQueryFilters()
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

        if (branch.IsDeleted)
        {
            return Result.Failure(BranchErrors.AlreadyDeleted);
        }

        // Check if there are any OrderFulfillments or StockTransactions referencing this branch
        var hasOrderFulfillments = await dbContext.OrderFulfillments
            .AnyAsync(of => of.BranchId == branch.Id, cancellationToken);
        var hasStockTransactions = await dbContext.StockAdjustments
            .AnyAsync(st => st.WarehouseId == branch.Id, cancellationToken);
        var hasPositiveIntories = await dbContext.Inventories
            .AnyAsync(i => i.WarehouseId == branch.Id && i.StockQuantity > 0, cancellationToken);

        if (hasOrderFulfillments || hasStockTransactions || hasPositiveIntories)
        {
            return Result.Failure(BranchErrors.CannotDeleteWithFulfillmentsOrInventoriesOrTransactions);
        }

        // Delete the branch address
        if (branch.Address != null)
        {
            dbContext.BranchAddresses.Remove(branch.Address);
        }

        // delete all related related inventories
        var inventories = await dbContext.Inventories
            .Where(i => i.WarehouseId == branch.Id)
            .ToListAsync(cancellationToken);

        dbContext.Inventories.RemoveRange(inventories);

        // Delete the branch
        dbContext.Branches.Remove(branch);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}