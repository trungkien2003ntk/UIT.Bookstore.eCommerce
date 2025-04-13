using AutoMapper;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;

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
    IMapper mapper
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