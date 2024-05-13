using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using MediatR;

namespace KKBookstore.Application.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<Result<SignInResponse>>
{
    public SignInRequest ToDto() => new(Email, Password);
}

public record SignInRequest(string Email, string Password);