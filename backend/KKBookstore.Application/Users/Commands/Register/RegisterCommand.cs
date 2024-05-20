using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    DateTimeOffset DateOfBirth,
    string Role
) : IRequest<Result<RegisterResponse>>;


