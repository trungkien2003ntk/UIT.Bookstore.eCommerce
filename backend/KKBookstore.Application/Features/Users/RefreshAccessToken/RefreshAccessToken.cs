using AutoMapper;
using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Users.RefreshAccessToken;

public record RefreshAccessToken(string RefreshToken)
    : IRequest<Result<RefreshAccessTokenResponse>>;

public class RefreshAccessTokenCommandHandler(
    IIdentityService identityService,
    IMapper mapper
) : IRequestHandler<RefreshAccessToken, Result<RefreshAccessTokenResponse>>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<RefreshAccessTokenResponse>> Handle(
        RefreshAccessToken request,
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
