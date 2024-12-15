using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Users;
using MediatR;

namespace KKBookstore.Application.Features.Users.SignIn;

public record SignInCommand(string Email, string Password, SignInSource SignInSource) : IRequest<Result<SignInResponse>>;

public class SignInCommandHandler(
    IIdentityService identityService
) : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    public async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await identityService.SignInAsync(request);
        if (signInResult.IsFailure)
        {
            return Result.Failure<SignInResponse>(signInResult.Error);
        }

        var result = signInResult.Value;
        var userInfo = result.BasicUserInfo;

        var response = Result.Success(new SignInResponse(
            result.AccessToken,
            result.AccessTokenExpiration,
            result.RefreshToken,
            new BasicUserInfoDto(userInfo.UserId, userInfo.ImageUrl, userInfo.FullName, userInfo.Email, userInfo.RoleName)
            ));

        return response;
    }
}