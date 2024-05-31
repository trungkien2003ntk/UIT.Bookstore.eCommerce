using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<Result<SignInResponse>>;

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