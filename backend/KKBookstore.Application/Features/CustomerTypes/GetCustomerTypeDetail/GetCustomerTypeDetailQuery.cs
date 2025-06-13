using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Features.CustomerTypes.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.CustomerTypes.GetCustomerTypeDetail;

public record GetCustomerTypeDetailQuery(int Id) : IRequest<Result<CustomerTypeDetail>>;

public class GetCustomerTypeDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerTypeDetailQuery, Result<CustomerTypeDetail>>
{
    public async Task<Result<CustomerTypeDetail>> Handle(GetCustomerTypeDetailQuery request, CancellationToken cancellationToken)
    {
        var customerType = await dbContext.CustomerTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken);

        if (customerType is null)
        {
            return Result.Failure<CustomerTypeDetail>(CustomerTypeErrors.NotFound);
        }
        var customerTypeDetail = new CustomerTypeDetail
        {
            Id = customerType.Id,
            Name = customerType.Name,
            Tier = customerType.Tier,
            MinSpending = (decimal)customerType.MinSpending,
            CreationTime = customerType.CreationTime,
            CreatorId = customerType.CreatorId,
            LastModificationTime = customerType.LastModificationTime,
            LastModifierId = customerType.LastModifierId,
            DeletionTime = customerType.DeletionTime,
            DeleterId = customerType.DeleterId
        };

        return Result.Success(customerTypeDetail);
    }
}
