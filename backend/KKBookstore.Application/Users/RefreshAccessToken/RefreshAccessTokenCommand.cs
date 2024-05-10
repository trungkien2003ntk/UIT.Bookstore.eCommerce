using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.RefreshAccessToken;

public record RefreshAccessTokenCommand(string Request)
    : IRequest<Result<RefreshAccessTokenResponse>>
{
    public RefreshAccessTokenRequest ToDto() => new(Request);
}

