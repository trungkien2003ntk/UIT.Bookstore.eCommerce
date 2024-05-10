using KKBookstore.Application.Users.CreateUser;
using KKBookstore.Application.Users.RefreshAccessToken;
using KKBookstore.Application.Users.SignIn;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;

namespace KKBookstore.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result> CreateUserAsync(CreateUserRequest request);
    Task<Result<SignInResponse>> SignInAsync(SignInRequest request);
    Task<Result<RefreshAccessTokenResponse>> RefreshAccessToken(RefreshAccessTokenRequest request);
    Task<bool> IsInRoleAsync(string userId, string role);
}