using AutoMapper;
using KKBookstore.Common.Interfaces;
using KKBookstore.Constants;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Users.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    DateTimeOffset DateOfBirth,
    Gender Gender,
    string Role
) : IRequest<Result<RegisterResponse>>;

public class RegisterCommandHandler(
    IIdentityService identityService,
    IMapper mapper,
    IApplicationDbContext dbContext
) : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<RegisterResponse>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await _identityService.CreateUserAsync(request);

            if (result.IsFailure)
            {
                return Result.Failure<RegisterResponse>(result.Error);
            }

            if (request.Role == AppRoles.Customer)
            {
                var customer = dbContext.Customers
                    .IgnoreQueryFilters()
                    .FirstOrDefault(c => c.Email == request.Email);
                var defaultCustomerType = dbContext.CustomerTypes
                    .IgnoreQueryFilters()
                    .FirstOrDefault(ct => ct.Tier == CustomerTier.Default);

                customer.CustomerTypeId = defaultCustomerType.Id;

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            RegisterResponse tokenResponse = mapper.Map<RegisterResponse>(result.Value);

            return Result.Success(tokenResponse);
        }
        catch
        {
            // TODO: add logging here

            throw;
        }
    }
}