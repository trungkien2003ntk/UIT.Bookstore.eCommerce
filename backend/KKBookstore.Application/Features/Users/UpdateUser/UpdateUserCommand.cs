using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.UpdateUser;

public record UpdateUserCommand : IRequest<Result>
{
    public int Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public string Role { get; init; }
}

public class UpdateUserCommandHandler(
    IIdentityService identityService,
    IMapper mapper
) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await identityService.UpdateUserAsync(request);

        if (updateResult.IsFailure)
        {
            return Result.Failure(updateResult.Error);
        }

        return updateResult;
    }
}
