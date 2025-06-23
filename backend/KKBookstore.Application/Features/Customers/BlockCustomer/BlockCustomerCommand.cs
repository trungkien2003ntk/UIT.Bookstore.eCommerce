using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.BlockCustomer;

public record BlockCustomerCommand(int Id, string Token) : IRequest<Result>;

public class BlockCustomerCommandHandler(
    IApplicationDbContext dbContext,
    ITokenBlacklistService tokenBlacklistService
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
            customer.Status = UserStatus.Blocked;

            // Blacklist the provided token
            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                var blacklistResult = await tokenBlacklistService.BlacklistTokenAsync(request.Token, cancellationToken);
                if (blacklistResult.IsFailure)
                {
                    return Result.Failure(blacklistResult.Error);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch
        {
            return Result.Failure(CustomerErrors.BlockFailed);
        }
    }
}