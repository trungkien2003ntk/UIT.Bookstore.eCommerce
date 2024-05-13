using KKBookstore.Application.Users.Commands.CreateUser;
using KKBookstore.Application.Users.Commands.RefreshAccessToken;
using KKBookstore.Application.Users.Commands.SignIn;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;

namespace KKBookstore.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<int>> CreateUserAsync(CreateUserRequest request);
    Task<Result<SignInResponse>> SignInAsync(SignInRequest request);
    Task<Result<RefreshAccessTokenResponse>> RefreshAccessToken(RefreshAccessTokenRequest request);
    Task<bool> IsInRoleAsync(int userId, string role);
}