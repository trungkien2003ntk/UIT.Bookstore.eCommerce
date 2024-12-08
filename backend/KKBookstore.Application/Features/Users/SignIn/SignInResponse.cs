using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Users.SignIn;

public record SignInResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken, BasicUserInfoDto BasicUserInfo)
{
}