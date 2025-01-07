using KKBookstore.Domain.Orders;

namespace KKBookstore.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendOtp(string email, string otp);
    Task SendOrderConfirmation(string email, string customerName, Order orderWithItems);
    Task SendOrderConfirmation(string email);
    Task SendOrderShippedEmailAsync(string email);
    Task SendPasswordResetLink(string email, string token);
}
