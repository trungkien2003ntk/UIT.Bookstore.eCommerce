namespace KKBookstore.Application.Users.Commands.Register;

public class RegisterResponse
{
    public int Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTimeOffset DateOfBirth { get; init; }
    public string Status { get; init; }
}
