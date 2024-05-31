using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Users.GetUserList;
using KKBookstore.Application.Features.Users.ReplaceUser;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.ReplaceUser
{
    public record ReplaceUserCommand : GetUserListResponse, IRequest<Result>;
}

public class ReplaceUserCommandHandler(
    IIdentityService identityService,
    IMapper mapper
) : IRequestHandler<ReplaceUserCommand, Result>
{
    public async Task<Result> Handle(ReplaceUserCommand request, CancellationToken cancellationToken)
    {
        var replaceResult = await identityService.ReplaceUserAsync(request);

        if (replaceResult.IsFailure)
        {
            return Result.Failure(replaceResult.Error);
        }

        return replaceResult;
    }
}
