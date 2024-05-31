namespace KKBookstore.Application.Features.Users.GetUser;

public class GetUserResponse
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Status { get; init; }
}