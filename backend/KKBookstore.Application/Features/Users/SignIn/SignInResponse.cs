using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Users.SignIn;

public record SignInResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken, BasicUserInfoDto BasicUserInfo)
{
}