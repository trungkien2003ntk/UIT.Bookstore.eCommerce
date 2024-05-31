using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Users.GetUserList;

public record GetUserListResponse : BaseDto
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Status { get; init; }
}
