using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ProductTypes.CreateProductTypeAttribute;

public record CreateProductTypeAttributeValueCommand(int ProductTypeId, string Name)
    : IRequest<Result<int>>;

public class CreateProductTypeAttributeValueHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<CreateProductTypeAttributeValueCommand, Result<int>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<int>> Handle(CreateProductTypeAttributeValueCommand request, CancellationToken cancellationToken)
    {
        // Validate product type exists
        var productType = await _dbContext.ProductTypes
            .FirstOrDefaultAsync(pt => pt.Id == request.ProductTypeId, cancellationToken);

        if (productType == null)
        {
            return Result.Failure<int>(ProductTypeErrors.NotFound);
        }

        // Check if attribute already exists
        var existingAttribute = await _dbContext.ProductTypeAttributes
            .FirstOrDefaultAsync(a => a.Name == request.Name, cancellationToken);

        // If attribute doesn't exist, create a new one
        if (existingAttribute == null)
        {
            existingAttribute = new ProductTypeAttribute
            {
                Name = request.Name
            };
            _dbContext.ProductTypeAttributes.Add(existingAttribute);
        }

        // Check if this attribute is already associated with the product type
        var isAlreadyAssociated = await _dbContext.ProductTypeAttributeMappings
            .AnyAsync(ptam =>
                ptam.ProductTypeId == request.ProductTypeId &&
                ptam.ProductTypeAttributeId == existingAttribute.Id,
                cancellationToken);

        if (isAlreadyAssociated)
        {
            return Result.Failure<int>(ProductTypeErrors.AttributeAlreadyAssociated);
        }

        // Create mapping between product type and attribute
        var mapping = new ProductTypeAttributeMapping
        {
            ProductTypeId = request.ProductTypeId,
            ProductTypeAttributeId = existingAttribute.Id
        };
        _dbContext.ProductTypeAttributeMappings.Add(mapping);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(existingAttribute.Id);
    }
}