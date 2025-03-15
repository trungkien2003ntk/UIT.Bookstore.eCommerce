namespace KKBookstore.API.Contracts.Requests.Users;

public record ChangePasswordRequest(string Email, string CurrentPassword, string NewPassword);
