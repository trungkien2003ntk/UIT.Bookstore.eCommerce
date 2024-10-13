using KKBookstore.Application.Features.Users.Register;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace KKBookstore.Application.Extensions;

public static class MappingExtension
{
    /// <summary>
    /// Converts an <see cref="IdentityResult"/> to an array of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="result">The <see cref="IdentityResult"/> to convert.</param>
    /// <returns>An array of <see cref="Error"/> objects.</returns>
    public static Error[] ToErrors(this IdentityResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToArray();
    }

    public static User ToEntity(this RegisterCommand request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            IsEmployee = false,
            IsAdmin = false,
            IsDeleted = false,
            IsActive = true,
            LoginType = LoginType.Email,
            Status = UserStatus.Active
        };
}
