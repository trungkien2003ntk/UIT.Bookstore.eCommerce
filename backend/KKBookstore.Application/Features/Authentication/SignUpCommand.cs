using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Application.Features.Users.Register;
using KKBookstore.Application.Features.Users.SignIn;
using KKBookstore.Domain.Authentication;
using KKBookstore.Domain.Constants;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Users;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Authentication;

public record SignUpCommand(
    string Token,
    string Password,
    string FullName,
    string? PhoneNumber,
    DateTimeOffset DateOfBirth,
    Gender Gender
) : IRequest<Result<SignInResponse>>;

public class SignUpCommandHandler(
    IIdentityService identityService,
    IApplicationDbContext dbContext,
    ILogger<SignUpCommandHandler> logger
) : IRequestHandler<SignUpCommand, Result<SignInResponse>>
{
    public async Task<Result<SignInResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        // Validate the registration token
        string? email = identityService.ValidateRegistrationToken(request.Token);
        if (email == null)
        {
            return Result.Failure<SignInResponse>(AuthErrors.InvalidToken);
        }

        // Check if user already exists
        var existingUser = await identityService.FindUserAsync(new(email));
        if (existingUser.IsSuccess)
        {
            return Result.Failure<SignInResponse>(UserErrors.AlreadyExists);
        }

        // Create a new user with Customer role
        var fullName = request.FullName?.Trim() ?? "";
        string firstName, lastName;
        UserHelper.ConvertFullNameToFirstAndLastName(fullName, out firstName, out lastName);
        var registerCommand = new RegisterCommand(
            FirstName: firstName, // These can be updated later by the user
            LastName: lastName,
            Email: email,
            PhoneNumber: request.PhoneNumber ?? "",
            Password: request.Password,
            DateOfBirth: DateTimeOffset.UtcNow, // Default value, can be updated later
            Gender: request.Gender,
            Role: Role.Customer
        );

        using (var transaction = await dbContext.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var createUserResult = await identityService.CreateUserAsync(registerCommand);
                if (createUserResult.IsFailure)
                {
                    return Result.Failure<SignInResponse>(createUserResult.Error);
                }
                logger.LogInformation("User {Email} created a new account", email);
                var result = createUserResult.Value;
                var userInfo = result.BasicUserInfo;

                // Return the same response format as SignInCommand
                var response = Result.Success(new SignInResponse(
                    result.AccessToken,
                    result.AccessTokenExpiration,
                    result.RefreshToken,
                    new BasicUserInfoDto(userInfo.UserId, userInfo.ImageUrl, userInfo.FullName, userInfo.Email, userInfo.RoleName)
                ));
                await transaction.CommitAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                logger.LogError(ex, "An error occurred while processing the sign-up request");
                return Result.Failure<SignInResponse>(AuthErrors.Unknown);
            }
        }
    }
}
