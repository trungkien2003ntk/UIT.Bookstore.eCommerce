using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.InitiateRegistration;

public record InitiateRegistrationCommand(string Email) : IRequest<Result>
{
}
