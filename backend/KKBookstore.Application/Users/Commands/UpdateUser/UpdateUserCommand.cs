using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<Result>
{
    public int Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public string Role { get; init; }
}
