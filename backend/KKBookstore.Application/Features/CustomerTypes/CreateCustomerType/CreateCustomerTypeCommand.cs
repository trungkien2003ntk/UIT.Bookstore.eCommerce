using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Features.CustomerTypes.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.CustomerTypes.CreateCustomerType;

public record CreateCustomerTypeCommand(
    string Name,
    CustomerTier Tier,
    double MinSpending
) : IRequest<Result<CustomerTypeDetail>>;

public class CreateCustomerTypeCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<CreateCustomerTypeCommand, Result<CustomerTypeDetail>>
{
    public async Task<Result<CustomerTypeDetail>> Handle(CreateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        // Check for duplicate customer type name
        if (await dbContext.CustomerTypes.AnyAsync(ct => ct.Name == request.Name, cancellationToken))
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.DuplicateCustomerTypeName(request.Name));
        }

        // Validate MinSpending is not negative
        if (request.MinSpending < 0)
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.InvalidAttribute("MinSpending cannot be negative"));
        }

        // Create the customer type
        var customerType = new CustomerType
        {
            Name = request.Name,
            Tier = request.Tier,
            MinSpending = request.MinSpending
        };

        dbContext.CustomerTypes.Add(customerType);
        await dbContext.SaveChangesAsync(cancellationToken);        // Return the created customer type details
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
