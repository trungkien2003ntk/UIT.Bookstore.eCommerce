using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ProductTypeAttributes.DeleteProductAttributeValue;

public record DeleteProductAttributeValueCommand(int ProductTypeAttributeId, int ProductTypeAttributeValueId) : IRequest<Result>;

public class DeleteProductAttributeValueCommandHandler : IRequestHandler<DeleteProductAttributeValueCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductAttributeValueCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteProductAttributeValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTypeAttributeValues
            .Where(x => x.Id == request.ProductTypeAttributeValueId && x.ProductTypeAttributeId == request.ProductTypeAttributeId)
            .Include(x => x.ProductsAppliedValue)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return Result.Failure(ProductTypeAttributeErrors.ValueNotFound);
        }

        if (entity.ProductsAppliedValue.Count > 1)
        {
            return Result.Failure(ProductTypeAttributeErrors.ValueInUse);
        }

        _context.ProductTypeAttributeValues.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}