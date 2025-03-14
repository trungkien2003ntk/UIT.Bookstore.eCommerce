using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.DeleteShippingAddress;

public record DeleteShippingAddressCommand(int Id) : IRequest<Result>;

public class DeleteShippingAddressCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteShippingAddressCommand, Result>
{
    public async Task<Result> Handle(DeleteShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = await dbContext.ShippingAddresses.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (shippingAddress is null)
        {
            return Result.Failure(UserErrors.ShippingAddressNotFound);
        }

        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            dbContext.ShippingAddresses.Remove(shippingAddress);
            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(UserErrors.DeleteShippingAddressFailed);
        }

    }
}
