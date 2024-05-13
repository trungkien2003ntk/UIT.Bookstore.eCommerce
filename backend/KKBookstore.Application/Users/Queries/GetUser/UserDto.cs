using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Users.Queries.GetUser;

public record UserDto : BaseDto
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Status { get; init; }
}
