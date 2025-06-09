using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.Responses;
using KKBookstore.Features.Users.ChangePassword;
using KKBookstore.Features.Users.RefreshAccessToken;
using KKBookstore.Features.Users.Register;
using KKBookstore.Features.Users.SignIn;
using KKBookstore.Features.Users.UpdateUser;
using KKBookstore.Features.Users.UpdateUserPartial;
using KKBookstore.Models;
using KKBookstore.Users;

namespace KKBookstore.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> GenerateResetPasswordTokenAsync(string email);
    Task<Result> ResetPasswordAsync(string email, string token, string newPassword);
    Task<Result<List<User>>> GetUsersInRoleAsync(string role);
    Task<Result<User>> FindUserAsync(FindUserRequest findUserDto);
    Task<Result<User>> FindUserByPhoneNumberAsync(string phoneNumber);
    Task<Result<User>> CreateTemporaryCustomerAsync(string email);
    Task<Result<AuthenticationResponse>> CreateUserAsync(RegisterCommand request);
    Task<Result> UpdateUserPartialAsync(UpdateUserPartialCommand command);
    Task<Result> UpdateUserAsync(UpdateUserCommand request);
    Task<Result<AuthenticationResponse>> SignInAsync(SignInCommand request);
    Task<Result<AuthenticationResponse>> GenerateJwtToken(string email);
    Task<Result<AuthenticationResponse>> RefreshAccessToken(RefreshAccessToken request);
    Task<bool> IsInRoleAsync(int userId, string role);
    Task<Result> ChangePasswordAsync(ChangePasswordCommand request);
    string GenerateRegistrationToken(string email);
    string? ValidateRegistrationToken(string token);
}