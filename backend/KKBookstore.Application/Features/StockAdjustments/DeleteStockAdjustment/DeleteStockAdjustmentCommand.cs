using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.DeleteStockAdjustment;

public record DeleteStockAdjustmentCommand(int Id) : IRequest<Result<bool>>;

public class DeleteStockAdjustmentCommandHandler : IRequestHandler<DeleteStockAdjustmentCommand, Result<bool>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public DeleteStockAdjustmentCommandHandler(
        IApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Result<bool>> Handle(
        DeleteStockAdjustmentCommand request, 
        CancellationToken cancellationToken)
    {
        var stockAdjustment = await _dbContext.StockAdjustments
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (stockAdjustment == null)
        {
            return Result.Failure<bool>(
                Error.NotFound("StockAdjustment.NotFound", 
                    "Stock adjustment with specified ID was not found"));
        }

        // Only allow deletion if the status is Pending
        if (stockAdjustment.TransactionStatus != StockTransactionStatus.Pending)
        {
            return Result.Failure<bool>(
                Error.Validation("StockAdjustment.CannotDelete", 
                    "Only pending stock adjustments can be deleted"));
        }

        // Perform soft delete instead of hard delete
        stockAdjustment.IsDeleted = true;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(true);
    }
}