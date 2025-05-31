using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Features.CustomerTypes.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.CustomerTypes.UpdateCustomerType;

public record UpdateCustomerTypeCommand(
    int Id,
    string Name,
    CustomerTier Tier,
    double MinSpending
) : IRequest<Result<CustomerTypeDetail>>;

public class UpdateCustomerTypeCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<UpdateCustomerTypeCommand, Result<CustomerTypeDetail>>
{
    public async Task<Result<CustomerTypeDetail>> Handle(UpdateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var customerType = await dbContext.CustomerTypes
            .FirstOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken);

        if (customerType == null)
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.NotFound);
        }

        // Check for duplicate customer type name (excluding current customer type)
        if (await dbContext.CustomerTypes.AnyAsync(ct => ct.Name == request.Name && ct.Id != request.Id, cancellationToken))
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.DuplicateCustomerTypeName(request.Name));
        }

        // Validate MinSpending is not negative
        if (request.MinSpending < 0)
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.InvalidAttribute("MinSpending cannot be negative"));
        }

        // Update customer type details
        customerType.Name = request.Name;
        customerType.Tier = request.Tier;
        customerType.MinSpending = request.MinSpending;

        dbContext.CustomerTypes.Update(customerType);
        await dbContext.SaveChangesAsync(cancellationToken);        // Return updated customer type details
        var customerTypeDetail = new CustomerTypeDetail
        {
            Id = customerType.Id,
            Name = customerType.Name,
            Tier = customerType.Tier,
            MinSpending = (decimal)customerType.MinSpending,
            CreationTime = customerType.CreationTime,
            CreatorId = customerType.CreatorId,
            LastModificationTime = customerType.LastModificationTime,
            LastModifierId = customerType.LastModifierId
        };

        return Result.Success(customerTypeDetail);
    }
}
