using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Application.Users.Commands.CreateUser;
using KKBookstore.Application.Users.Commands.RefreshAccessToken;
using KKBookstore.Application.Users.Commands.SignIn;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KKBookstore.Infrastructure.Identity;

public class IdentityService(
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    IPasswordHasher<User> passwordHasher,
    IOptions<JwtSettings> jwtSettings,
    ApplicationDbContext dbContext
) : IIdentityService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IOptions<JwtSettings> _jwtSettings = jwtSettings;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<User>> FindUserAsync(FindUserDto findUserDto)
    {
        var user = await _userManager.FindByEmailAsync(findUserDto.Email);

        return user == null
            ? Result.Failure<User>(UserErrors.NotFound)
            : Result.Success(user);
    }

    public async Task<Result<int>> CreateUserAsync(CreateUserRequest request)
    {
        var searchResult = await FindUserAsync(new(request.Email));
        if (searchResult.IsSuccess)
        {
            return Result.Failure<int>(UserErrors.AlreadyExists);
        }

        var role = await _roleManager.FindByNameAsync(request.Role);
        if (role == null)
        {
            return Result.Failure<int>(UserErrors.InvalidRole);
        }

        var user = request.ToEntity();
        user.LoginType = LoginType.Email;
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.ToErrors();
            return Result.Failure<int>(errors.FirstOrDefault() ?? UserErrors.Unknown);
        }

        await _userManager.AddToRoleAsync(user, role.Name!);
        
        return Result.Success(user.Id);
    }

    public async Task<Result<SignInResponse>> SignInAsync(SignInRequest request)
    {
        var result = await FindUserAsync(new(request.Email));
        if (result.IsFailure)
        {
            return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
        }

        var user = result.Value;
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Result.Failure<SignInResponse>(UserErrors.InvalidCredentials);
        }

        // get user roles to include inside the claims
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count == 0)
        {
            return Result.Failure<SignInResponse>(UserErrors.MissingRole);
        }

        JwtSecurityToken accessToken = await GenerateAccessToken(user);
        var expires = accessToken.ValidTo;

        var refreshTokenResult = await GetRefreshToken(user.Id);
        if (refreshTokenResult.IsFailure)
        {
            return Result.Failure<SignInResponse>(refreshTokenResult.Error);
        }

        var refreshToken = refreshTokenResult.Value;

        return Result.Success(new SignInResponse(
            AccessToken : new JwtSecurityTokenHandler().WriteToken(accessToken),
            AccessTokenExpiration : expires,
            RefreshToken : refreshToken.Token
        ));
    }

    public async Task<Result<RefreshAccessTokenResponse>> RefreshAccessToken(RefreshAccessTokenRequest request)
    {
        // get the user send this
        var existingRefreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.ExpiryDate > DateTimeOffset.UtcNow);

        if (existingRefreshToken == null)
        {
            return Result.Failure<RefreshAccessTokenResponse>(TokenErrors.InvalidRefreshToken);
        }

        var user = await _userManager.FindByIdAsync(existingRefreshToken.UserId.ToString());
        var accessToken = await GenerateAccessToken(user!);
        string writtenToken = ConvertJwtTokenToString(accessToken);
        var response = new RefreshAccessTokenResponse(writtenToken);

        return Result.Success(response);
    }

    public async Task<bool> IsValidRefreshToken(int userId, string refreshToken)
    {
        var utcNow = DateTimeOffset.UtcNow;

        var existingRefreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.ExpiryDate > utcNow);

        if (existingRefreshToken != null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    private async Task<JwtSecurityToken> GenerateAccessToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Name, user.Email!),
        new(ClaimTypes.Role, roles.FirstOrDefault()!),
        // Add other claims as needed...
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.Value.AccessExpirationInMinutes));

        var token = new JwtSecurityToken(
            _jwtSettings.Value.Issuer,
            _jwtSettings.Value.Issuer,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return token;
    }

    private async Task<Result<RefreshToken>> GetRefreshToken(int userId)
    {
        var existingRefreshToken = await FindExistingRefreshToken(userId);

        if (existingRefreshToken != null)
        {
            return existingRefreshToken;
        }

        await RemoveExpiredRefreshTokens(userId);

        return await CreateAndSaveNewRefreshToken(userId);
    }

    private async Task<RefreshToken?> FindExistingRefreshToken(int userId)
    {
        var utcNow = DateTimeOffset.UtcNow;
        return await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.ExpiryDate > utcNow);
    }

    private async Task RemoveExpiredRefreshTokens(int userId)
    {
        var utcNow = DateTimeOffset.UtcNow;
        var invalidRefreshTokens = await _dbContext.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.ExpiryDate < utcNow)
            .ToListAsync();

        _dbContext.RefreshTokens.RemoveRange(invalidRefreshTokens);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Result<RefreshToken>> CreateAndSaveNewRefreshToken(int userId)
    {
        var refreshTokenResult = RefreshToken.Create(userId, _jwtSettings.Value.RefreshExpirationInMonths);
        if (refreshTokenResult.IsFailure)
        {
            return Result.Failure<RefreshToken>(refreshTokenResult.Error);
        }

        var refreshToken = refreshTokenResult.Value;
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshToken;
    }

    private static string ConvertJwtTokenToString(JwtSecurityToken accessToken)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var writtenToken = jwtHandler.WriteToken(accessToken);
        return writtenToken;
    }
}