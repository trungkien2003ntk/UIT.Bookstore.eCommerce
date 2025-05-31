using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.CustomerTypes.DeleteCustomerType;

public record DeleteCustomerTypeCommand(int Id) : IRequest<Result>;

public class DeleteCustomerTypeCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteCustomerTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var customerType = await dbContext.CustomerTypes
            .FirstOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken);

        if (customerType == null)
        {
            return Result.Failure(CustomerTypeErrors.NotFound);
        }

        // Check if customer type is being used by any customers
        var isInUse = await dbContext.Customers
            .AnyAsync(c => c.CustomerTypeId == request.Id, cancellationToken);

        if (isInUse)
        {
            return Result.Failure(CustomerTypeErrors.InvalidAttribute("Cannot delete customer type that is in use by customers"));
        }

        // Delete the customer type
        dbContext.CustomerTypes.Remove(customerType);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
