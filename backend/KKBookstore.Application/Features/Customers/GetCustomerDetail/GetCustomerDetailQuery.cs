// filepath: d:\Github\Bookstore-ECommerce\backend\KKBookstore.Application\Features\Customers\GetCustomerDetail\GetCustomerDetailQuery.cs
using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Features.Customers.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.GetCustomerDetail;

public record GetCustomerDetailQuery(int Id) : IRequest<Result<CustomerDetail>>;

public class GetCustomerDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetCustomerDetailQuery, Result<CustomerDetail>>
{
    public async Task<Result<CustomerDetail>> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Users
            .OfType<Customer>()
            .AsNoTracking()
            .Include(c => c.CustomerType)
            .Include(c => c.ShippingAddresses)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerDetail>(CustomerErrors.NotFound);
        }

        var totalSpent = customer.TotalSpent;

        var customerDetail = new CustomerDetail
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            FullName = customer.FullName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber ?? string.Empty,
            DateOfBirth = customer.DateOfBirth,
            Gender = customer.Gender,
            ImageUrl = customer.ImageUrl,
            IsActive = customer.IsActive,
            IsDeleted = customer.IsDeleted,
            Status = customer.Status,
            CustomerTypeId = customer.CustomerTypeId,
            CustomerTypeName = customer.CustomerType?.Name,
            TotalSpent = totalSpent,
            CreationTime = customer.CreationTime,
            CreatorId = customer.CreatorId,
            LastModificationTime = customer.LastModificationTime,
            LastModifierId = customer.LastModifierId
        };

        if (customer.ShippingAddresses != null && customer.ShippingAddresses.Any())
        {
            customerDetail.ShippingAddresses = customer.ShippingAddresses.Select(a => new ShippingAddressSummary
            {
                Id = a.Id,
                ReceiverName = a.ReceiverName,
                PhoneNumber = a.PhoneNumber,
                ProvinceId = a.ProvinceId,
                ProvinceName = a.ProvinceName,
                DistrictId = a.DistrictId,
                DistrictName = a.DistrictName,
                CommuneCode = a.CommuneCode,
                CommuneName = a.CommuneName,
                DetailAddress = a.DetailAddress,
                IsDefault = a.IsDefault,
                FormattedAddress = $"{a.DetailAddress}, {a.CommuneName}, {a.DistrictName}, {a.ProvinceName}"
            }).ToList();
        }

        return Result.Success(customerDetail);
    }
}
