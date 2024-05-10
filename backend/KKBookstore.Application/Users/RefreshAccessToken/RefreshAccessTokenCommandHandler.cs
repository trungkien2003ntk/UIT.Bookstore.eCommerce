using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.RefreshAccessToken;

public class RefreshAccessTokenCommandHandler(
    IIdentityService identityService
): IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAccessTokenResponse>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<RefreshAccessTokenResponse>> Handle(
        RefreshAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _identityService.RefreshAccessToken(request.ToDto());

        if (result.IsFailure)
        {
            return Result.Failure<RefreshAccessTokenResponse>(result.Error);
        }

        return result;

    }
}
