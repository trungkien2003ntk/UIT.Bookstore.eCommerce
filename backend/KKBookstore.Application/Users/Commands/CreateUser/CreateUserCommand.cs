using MediatR;
using KKBookstore.Domain.Common;

namespace KKBookstore.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FullName,
    string Email,
    string Password,
    string Role
) : IRequest<Result<int>>
{
    public CreateUserRequest ToDto() => new(
        FullName,
        Email,
        Password,
        Role
    );
}


