using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.SignIn;

public class SignInCommandHandler(
    IIdentityService identityService,
    IMapper mapper
) : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    public async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await identityService.SignInAsync(request);
        if (signInResult.IsFailure)
        {
            return Result.Failure<SignInResponse>(signInResult.Error);
        }

        var response = mapper.Map<SignInResponse>(signInResult.Value);

        return response;
    }
}