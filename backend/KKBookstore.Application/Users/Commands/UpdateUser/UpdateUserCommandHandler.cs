using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.UpdateUser;

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
