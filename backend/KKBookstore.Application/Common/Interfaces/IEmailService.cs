using KKBookstore.Domain.Aggregates.OrderAggregate;

namespace KKBookstore.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendOtp(string email, string otp);
    Task SendOrderConfirmation(string email, string customerName, Order orderWithItems);
}
