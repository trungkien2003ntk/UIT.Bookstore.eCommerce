namespace KKBookstore.API.Contracts.Requests.Auth;

public record ResetPasswordRequest(string Token, string NewPassword);
