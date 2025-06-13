using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.DiscountVoucherAction;

public record DiscountVoucherActionCommand : IRequest<Result>
{
    public int Id { get; init; }
    public DiscountVoucherActionType Action { get; init; }
}

public class DiscountVoucherActionCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DiscountVoucherActionCommand, Result>
{
    public async Task<Result> Handle(DiscountVoucherActionCommand request, CancellationToken cancellationToken)
    {
        var discountVoucher = await dbContext.DiscountVouchers
            .FirstOrDefaultAsync(dv => dv.Id == request.Id, cancellationToken);

        if (discountVoucher == null)
        {
            return Result.Failure(DiscountVoucherErrors.NotFound);
        }

        // Use domain method to update status
        var updateResult = discountVoucher.UpdateStatus(request.Action);
        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
