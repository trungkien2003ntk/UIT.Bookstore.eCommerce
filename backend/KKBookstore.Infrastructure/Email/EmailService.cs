using KKBookstore.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace KKBookstore.Infrastructure.Email;

public class EmailService(
    IOptions<EmailConfiguration> emailConfiguration
) : IEmailService
{
    private readonly IOptions<EmailConfiguration> _emailConfig = emailConfiguration;

    public async Task SendOtp(string email, string otp)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Administrator", _emailConfig.Value.From));
        mailMessage.To.Add(new MailboxAddress("Customer", email));
        mailMessage.Subject = "Your One-Time Passcode(OTP) for KKBookstore";
        mailMessage.Body = new TextPart("plain")
        {
            Text = @$"Dear our valued customer,
Thank you for requesting a one - time passcode(OTP) for your KKBookstore account.

Your OTP is: {otp}

Please enter this code within the next 5 minutes.

For security purposes, please do not share this code with anyone.

If you did not request an OTP, please disregard this email. Your account security is important to us. If you suspect any unauthorized activity, please contact us immediately at {_emailConfig.Value.From}.

Sincerely,

The KKBookstore Team"
        };
        
        
        await Send(mailMessage);
    }
    private async Task Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(_emailConfig.Value.SmtpServer, _emailConfig.Value.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(_emailConfig.Value.UserName, _emailConfig.Value.Password);
        await client.SendAsync(mailMessage);
    }
}
