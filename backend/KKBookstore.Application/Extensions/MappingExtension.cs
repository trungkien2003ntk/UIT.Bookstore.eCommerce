using KKBookstore.Application.Users.Commands.CreateUser;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
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

    public static User ToEntity(this CreateUserRequest dto) =>
        new()
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };
}
