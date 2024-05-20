using KKBookstore.Application.Users.Queries.Models;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.ReplaceUser
{
    public record ReplaceUserCommand : UserDto, IRequest<Result>;
}
