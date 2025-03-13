using KKBookstore.Domain.Emailing.Models;
using KKBookstore.Domain.Orders;

namespace KKBookstore.Domain.Emailing;

public interface IEmailSender
{
    //
    // Summary:
    //     Sends an email.
    Task SendAsync(string to, string? subject, string? body, bool isBodyHtml = true, AdditionalEmailSendingArgs? additionalEmailSendingArgs = null);

    //
    // Summary:
    //     Sends an email.
    Task SendAsync(string from, string to, string? subject, string? body, bool isBodyHtml = true, AdditionalEmailSendingArgs? additionalEmailSendingArgs = null);

    Task SendOtp(string email, string otp);
    Task SendOrderConfirmation(string email, string customerName, Order orderWithItems);
    Task SendOrderConfirmation(string email);
    Task SendOrderShippedEmailAsync(string email);
    Task SendPasswordResetLink(string email, string token);
}
