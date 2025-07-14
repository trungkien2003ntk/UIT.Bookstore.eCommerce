using KKBookstore.Common.Interfaces;
using KKBookstore.Constants;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Users.GetUser;

public record GetUserQuery(int UserId) : IRequest<Result<GetUserResponse>>;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
{
    public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.Status == UserStatus.Active && u.IsActive)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetUserResponse>(UserErrors.NotFound);
        }

        var userRoles = await (
                from userRole in dbContext.UserRoles
                join role in dbContext.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == user.Id
                select role.Name
            ).ToListAsync(cancellationToken);

        CustomerTypeDto? customerType = null;
        decimal? totalSpent = null;
        CustomerTier? spendingTier = null;

        if (userRoles.Any() && userRoles.Contains(AppRoles.Customer))
        {
            customerType = await dbContext.Customers
                .Include(c => c.CustomerType)
                .Where(ct => ct.Id == user.Id && ct.CustomerTypeId != null)
                .Select(ct => new CustomerTypeDto
                {
                    Id = ct.CustomerTypeId,
                    Name = ct.CustomerType.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            var customer = await dbContext.Customers.SingleAsync(u => u.Id == user.Id);
            totalSpent = customer.TotalSpent;

            // Determine spending tier based on total spent amount
            if (totalSpent.HasValue)
            {
                var customerTypes = await dbContext.CustomerTypes
                    .OrderByDescending(ct => ct.MinSpending)
                    .ToListAsync(cancellationToken);

                var applicableCustomerType = customerTypes
                    .FirstOrDefault(ct => totalSpent.Value >= (decimal)ct.MinSpending);

                spendingTier = applicableCustomerType?.Tier ?? CustomerTier.Default;
            }
        }

        var userDto = new GetUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            FullName = user.FullName,
            Status = user.Status.ToString(),
            ImageUrl = user.ImageUrl,
            Roles = userRoles,
            CustomerType = customerType,
            TotalSpent = totalSpent,
            SpendingTier = spendingTier
        };

        return Result.Success(userDto);
    }
}
