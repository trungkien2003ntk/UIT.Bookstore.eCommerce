using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Products.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<Result>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DeleteProductCommandHandler> _logger;

    public DeleteProductCommandHandler(IApplicationDbContext dbContext, ILogger<DeleteProductCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        _logger.LogInformation("Deleting product with ID {ProductId}", request.Id);

        // Since AuditingInterceptor handles soft delete, we just need to call Remove
        // which will trigger EntityState.Deleted and the interceptor will handle the soft delete
        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully deleted product with ID {ProductId}", request.Id);

        return Result.Success();
    }
}
