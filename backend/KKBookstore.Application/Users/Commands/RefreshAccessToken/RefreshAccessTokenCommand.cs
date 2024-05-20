using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.RefreshAccessToken;

public record RefreshAccessTokenCommand(string RefreshToken)
    : IRequest<Result<RefreshAccessTokenResponse>>;

