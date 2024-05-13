namespace KKBookstore.Application.Users.Commands.CreateUser;

public record CreateUserRequest(
    string FullName,
    string Email,
    string Password,
    string Role
);
