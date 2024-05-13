using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Queries.GetUser;

public record GetUserQuery(int UserId) : IRequest<Result<UserDto>>
{
    public GetUserRequest ToDto() => new(UserId);
}
