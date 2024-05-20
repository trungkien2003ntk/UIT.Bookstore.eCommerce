using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<Result<SignInResponse>>;