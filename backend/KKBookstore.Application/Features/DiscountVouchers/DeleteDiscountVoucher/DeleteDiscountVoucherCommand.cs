using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.DeleteDiscountVoucher;

public record DeleteDiscountVoucherCommand(int Id) : IRequest<Result>;

public class DeleteDiscountVoucherCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteDiscountVoucherCommand, Result>
{
    public async Task<Result> Handle(DeleteDiscountVoucherCommand request, CancellationToken cancellationToken)
    {
        var discountVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.VoucherUsages)
            .FirstOrDefaultAsync(dv => dv.Id == request.Id, cancellationToken); if (discountVoucher == null)
        {
            return Result.Failure(DiscountVoucherErrors.NotFound);
        }

        // Check if voucher has been used
        if (discountVoucher.VoucherUsages.Any())
        {
            return Result.Failure(Error.BusinessRuleViolation(
                "DiscountVoucher.CannotDeleteUsed",
                "Cannot delete a voucher that has been used"));
        }

        // Only allow deletion of Draft or Cancelled vouchers
        if (discountVoucher.Status != DiscountStatus.Draft && discountVoucher.Status != DiscountStatus.Cancelled)
        {
            return Result.Failure(Error.BusinessRuleViolation(
                "DiscountVoucher.CannotDeleteActive",
                "Can only delete draft or cancelled vouchers"));
        }

        dbContext.DiscountVouchers.Remove(discountVoucher);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
