using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Users.GetUserList;

public record GetUserListResponse : BaseDto
{
    public string FullName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Status { get; init; }
    public string? ImageUrl { get; init; }
}
