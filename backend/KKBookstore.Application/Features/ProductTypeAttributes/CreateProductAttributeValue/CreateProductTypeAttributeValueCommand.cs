using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.ProductTypeAttributes.CreateProductAttributeValue;

public record CreateProductTypeAttributeValueCommand(int ProductTypeAttributeId, string Value) : IRequest<Result<int>>;

public class CreateProductTypeAttributeValueCommandHandler : IRequestHandler<CreateProductTypeAttributeValueCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateProductTypeAttributeValueCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateProductTypeAttributeValueCommand request, CancellationToken cancellationToken)
    {
        var existingValue = await _context.ProductTypeAttributeValues
            .FirstOrDefaultAsync(x => x.ProductTypeAttributeId == request.ProductTypeAttributeId && x.Value == request.Value, cancellationToken);

        if (existingValue != null)
        {
            return existingValue.Id;
        }

        var entity = new ProductTypeAttributeValue
        {
            ProductTypeAttributeId = request.ProductTypeAttributeId,
            Value = request.Value
        };

        _context.ProductTypeAttributeValues.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}