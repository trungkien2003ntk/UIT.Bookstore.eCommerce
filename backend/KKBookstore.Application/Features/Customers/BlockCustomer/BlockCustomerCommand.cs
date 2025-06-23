using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.BlockCustomer;

public record BlockCustomerCommand(int Id) : IRequest<Result>;

public class BlockCustomerCommandHandler(
    IApplicationDbContext dbContext,
    ITokenVersionService tokenVersionService
) : IRequestHandler<BlockCustomerCommand, Result>
{
    public async Task<Result> Handle(BlockCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Users
            .OfType<Customer>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound);
        }

        // Check if customer is already blocked/inactive
        if (customer.Status == UserStatus.Blocked)
        {
            return Result.Failure(CustomerErrors.AlreadyBlocked);
        }
        try
        {
            // Set customer status to Blocked (blocked)
            // This also regenerates TokenVersion which invalidates all existing tokens
            customer.MarkAsBlocked();

            await dbContext.SaveChangesAsync(cancellationToken);

            // Invalidate cached token version to ensure immediate effect
            await tokenVersionService.InvalidateTokenVersionCacheAsync(customer.Id, cancellationToken);

            return Result.Success();
        }
        catch
        {
            return Result.Failure(CustomerErrors.BlockFailed);
        }
    }
}