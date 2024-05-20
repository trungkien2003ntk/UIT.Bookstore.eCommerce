using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.ReplaceUser;

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
