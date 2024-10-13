using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Email.EmailTemplates;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace KKBookstore.Infrastructure.Email;

public class EmailService(
    IOptions<EmailConfiguration> emailConfiguration,
    QueueServiceClient queueServiceClient,
    BlobServiceClient blobServiceClient
) : IEmailService
{
    private readonly string SenderName = "KKBookstore";
    private readonly string ReceiverName = "Customer";
    private readonly string _queueName = "mail-trigger";
    private readonly string _blobContainerName = "mail-body";

    private readonly EmailConfiguration _emailConfig = emailConfiguration.Value;
    private readonly QueueServiceClient _queueServiceClient = queueServiceClient;
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;

    // email templates
    private readonly EmailTemplate orderConfirmationTemplate = new()
    {
        Subject = "Your Order Confirmation - KKBookstore"
    };

    private readonly EmailTemplate sendOtpEmailTemplate = new()
    {
        Subject = "Your One-Time Passcode(OTP) - KKBookstore",
        Body = @"<!DOCTYPE html>
<html>
<head>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
            border-radius: 5px;
        }

        h1 {
            color: #007bff; /* Blue accent color */
        }

        .otp {
            font-size: 24px;
            font-weight: bold;
            background-color: #fff;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Dear our valued customer,</h1>
        <p>Thank you for requesting a one-time passcode (OTP) for your KKBookstore account.</p>
        <div class=""otp"">Your OTP is: {otp}</div>
        <p>Please enter this code within the next 5 minutes.</p>

        <h2>For security purposes, please do not share this code with anyone.</h2>

        <p>If you did not request an OTP, please disregard this email. Your account security is important to us. If you suspect any unauthorized activity, please contact us immediately at <a href=""mailto:{AdminEmail}"">{AdminEmail}</a>.</p>

        <p>Sincerely,</p>
        <p>The KKBookstore Team</p>
    </div>
</body>
</html>"
    };

    public async Task SendOtp(string email, string otp)
    {
        var message = CreateEmailMessage(
            sendOtpEmailTemplate.Subject,
            sendOtpEmailTemplate.Body,
            email,
            new Dictionary<string, string>()
            {
                { "{otp}", otp},
                { "{AdminEmail}", _emailConfig.From},
                { "{CustomerEmail}", email},
            }
        );

        await Send(message);
    }

    public async Task SendOrderConfirmation(string email, string customerName, Order orderWithItems)
    {
        orderConfirmationTemplate.Body = EmailBodyHelper.BuildOrderConfirmationEmailBody(orderWithItems);

        var message = CreateEmailMessage(
            orderConfirmationTemplate.Subject,
            orderConfirmationTemplate.Body,
            email,
            new Dictionary<string, string>()
            {
                { "{AdminEmail}", _emailConfig.From },
                { "{CustomerEmail}", email }
            }
        );

        await Send(message);
    }

    private MimeMessage CreateEmailMessage(string subject, string body, string toEmail, Dictionary<string, string>? placeholders = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(SenderName, _emailConfig.From));
        message.To.Add(new MailboxAddress(ReceiverName, toEmail));
        message.Subject = subject;


        if (placeholders != null)
        {
            foreach (var placeholder in placeholders)
            {
                body = body.Replace(placeholder.Key, placeholder.Value);
            }
        }

        message.Body = new TextPart("plain") { Text = body };

        return message;
    }

    private async Task Send(MimeMessage mailMessage)
    {
        var messageData = SerializeMimeMessage(mailMessage);

        var queueClient = _queueServiceClient.GetQueueClient(_queueName);

        string blobUri;
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
        var blobName = "mail-body-" + Guid.NewGuid().ToString();

        using (var stream = new MemoryStream())
        {
            mailMessage.WriteTo(stream);

            var blobClient = blobContainerClient.GetBlobClient(blobName);
            stream.Position = 0;
            await blobClient.UploadAsync(stream);
        }

        // base64 encode the blobname before send:
        var blobNameBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(blobName));


        await queueClient.SendMessageAsync(blobNameBase64);
    }

    private static string SerializeMimeMessage(MimeMessage mailMessage)
    {
        using var stream = new MemoryStream();

        mailMessage.WriteTo(stream);

        var result = Convert.ToBase64String(stream.ToArray());
        return result;
    }
}
