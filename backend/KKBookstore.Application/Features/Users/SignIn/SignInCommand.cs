using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;

namespace KKBookstore.Features.Users.SignIn;

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