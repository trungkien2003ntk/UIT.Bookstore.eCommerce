using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.Register;

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

            RegisterResponse userDto = mapper.Map<RegisterResponse>(result.Value);

            return Result.Success(userDto);
        }
        catch
        {
            // TODO: add logging here

            throw;
        }
    }
}