using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockTransfers.DeleteStockTransfer;

public record DeleteStockTransferCommand(int Id) : IRequest<Result<bool>>;

public class DeleteStockTransferCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<DeleteStockTransferCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteStockTransferCommand request, CancellationToken cancellationToken)
    {
        var stockTransfer = await dbContext.StockTransfers
            .FirstOrDefaultAsync(st => st.Id == request.Id, cancellationToken);

        if (stockTransfer is null)
        {
            return Result.Failure<bool>(
                Error.NotFound("StockTransfer.NotFound", $"Stock transfer with ID {request.Id} was not found."));
        }

        if (stockTransfer.TransactionStatus != StockTransactionStatus.Pending)
        {
            return Result.Failure<bool>(
                Error.Validation("StockTransfer.CannotDelete", "Only pending stock transfers can be deleted."));
        }

        stockTransfer.IsDeleted = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(true);
    }
}
