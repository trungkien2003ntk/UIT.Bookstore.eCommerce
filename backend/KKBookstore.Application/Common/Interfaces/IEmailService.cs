namespace KKBookstore.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendOtp(string email, string otp);
}
