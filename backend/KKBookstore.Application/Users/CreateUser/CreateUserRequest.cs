namespace KKBookstore.Application.Users.CreateUser;

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password, string Role);
