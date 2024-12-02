using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Users.ChangePassword;
using KKBookstore.Application.Features.Users.RefreshAccessToken;
using KKBookstore.Application.Features.Users.Register;
using KKBookstore.Application.Features.Users.ReplaceUser;
using KKBookstore.Application.Features.Users.SignIn;
using KKBookstore.Application.Features.Users.UpdatePassword;
using KKBookstore.Application.Features.Users.UpdateUser;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;

namespace KKBookstore.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> GenerateResetPasswordTokenAsync(string email);
    Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
    Task<Result<User>> FindUserAsync(FindUserDto findUserDto);
    Task<Result<User>> CreateTemporaryCustomerAsync(string email);
    Task<Result<TokenResponse>> CreateUserAsync(RegisterCommand request);
    Task<Result> UpdateUserAsync(UpdateUserCommand command);
    Task<Result> ReplaceUserAsync(ReplaceUserCommand request);
    Task<Result<TokenResponse>> SignInAsync(SignInCommand request);
    Task<Result<TokenResponse>> GenerateJwtToken(string email);
    Task<Result<TokenResponse>> RefreshAccessToken(RefreshAccessToken request);
    Task<bool> IsInRoleAsync(int userId, string role);
    Task<Result> UpdatePasswordAsync(ResetPasswordCommand request);
    Task<Result> ChangePasswordAsync(ChangePasswordCommand request);
}