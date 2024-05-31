using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.UpdatePassword;

public record UpdatePasswordCommand(string Email, string NewPassword) : IRequest<Result>;

public class UpdatePasswordCommandHandler(
    IIdentityService identityService
) : IRequestHandler<UpdatePasswordCommand, Result>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.UpdatePasswordAsync(request);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
