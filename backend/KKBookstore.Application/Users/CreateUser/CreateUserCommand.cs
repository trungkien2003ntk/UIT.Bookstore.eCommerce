using MediatR;
using KKBookstore.Domain.Common;

namespace KKBookstore.Application.Users.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password, string Role)
    : IRequest<Result>
{
    public CreateUserRequest ToDto() => new(FirstName, LastName, Email, Password, Role);
}


