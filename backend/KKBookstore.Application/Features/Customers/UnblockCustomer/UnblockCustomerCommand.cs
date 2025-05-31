using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.UnblockCustomer;

public record UnblockCustomerCommand(int Id) : IRequest<Result>;

public class UnblockCustomerCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<UnblockCustomerCommand, Result>
{
    public async Task<Result> Handle(UnblockCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Users
            .OfType<Customer>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound);
        }

        // Check if customer is already active/unblocked
        if (customer.Status == UserStatus.Active)
        {
            return Result.Failure(CustomerErrors.AlreadyActive);
        }

        try
        {
            // Set customer status to Active (unblocked)
            customer.Status = UserStatus.Active;
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
        catch
        {
            return Result.Failure(CustomerErrors.UnblockFailed);
        }
    }
}
