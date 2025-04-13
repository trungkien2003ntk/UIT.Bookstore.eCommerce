namespace KKBookstore.Contracts.Requests.Auth;

public record ResetPasswordRequest(string Token, string NewPassword);
