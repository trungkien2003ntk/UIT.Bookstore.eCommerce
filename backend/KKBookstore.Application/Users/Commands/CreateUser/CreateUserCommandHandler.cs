using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IIdentityService identityService
) : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<int>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await _identityService.CreateUserAsync(request.ToDto());

            if (result.IsFailure)
            {
                return result;
            }

            return Result.Success(result.Value);
        }
        catch
        {
            // TODO: add logging here

            throw;
        }
    }
}