using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.RefreshAccessToken;

public class RefreshAccessTokenCommandHandler(
    IIdentityService identityService,
    IMapper mapper
) : IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAccessTokenResponse>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<RefreshAccessTokenResponse>> Handle(
        RefreshAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshResult = await _identityService.RefreshAccessToken(request);

        if (refreshResult.IsFailure)
        {
            return Result.Failure<RefreshAccessTokenResponse>(refreshResult.Error);
        }

        var response = mapper.Map<RefreshAccessTokenResponse>(refreshResult.Value);

        return response;

    }
}
