using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Orders.SendOrderEmail;

public enum EmailType
{
    OrderConfirmation,
    OrderShipped,
    OrderDelivered
}

public record SendOrderEmailCommand(EmailType EmailType, string Email) : IRequest<Result>;

public class SendOrderEmailCommandHandler(
    IEmailService emailService
) : IRequestHandler<SendOrderEmailCommand, Result>
{
    public async Task<Result> Handle(SendOrderEmailCommand request, CancellationToken cancellationToken)
    {
        // Send email based on the email type
        switch (request.EmailType)
        {
            case EmailType.OrderConfirmation:
                await emailService.SendOrderConfirmation(request.Email);
                break;
            case EmailType.OrderShipped:
                await emailService.SendOrderShippedEmailAsync(request.Email);
                break;
        }
        return Result.Success();
    }
}