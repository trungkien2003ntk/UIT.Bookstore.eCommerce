using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using KKBookstore.Application.Common.Constants;
using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Emailing.Models;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Emailing;
using KKBookstore.Infrastructure.Emailing.EmailTemplates;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace KKBookstore.Infrastructure.Emailing;

public class DefaultEmailSender(
    IOptions<EmailConfiguration> emailConfiguration,
    QueueServiceClient queueServiceClient,
    BlobServiceClient blobServiceClient
) : IEmailSender
{
    private readonly EmailConfiguration _emailConfig = emailConfiguration.Value;
    private readonly QueueServiceClient _queueServiceClient = queueServiceClient;
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;


    public async Task SendAsync(string to, string? subject, string? body, bool isBodyHtml = true, AdditionalEmailSendingArgs? additionalEmailSendingArgs = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailConsts.SenderName, _emailConfig.From));
        message.To.Add(new MailboxAddress(EmailConsts.ReceiverName, to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = isBodyHtml ? body : null,
            TextBody = !isBodyHtml ? body : null
        };

        ProcessAdditionalArgs(additionalEmailSendingArgs, message, bodyBuilder);

        message.Body = bodyBuilder.ToMessageBody();

        await SendAsync(message);
    }

    public async Task SendAsync(string from, string to, string? subject, string? body, bool isBodyHtml = true, AdditionalEmailSendingArgs? additionalEmailSendingArgs = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailConsts.SenderName, from));
        message.To.Add(new MailboxAddress(EmailConsts.ReceiverName, to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = isBodyHtml ? body : null,
            TextBody = !isBodyHtml ? body : null
        };

        ProcessAdditionalArgs(additionalEmailSendingArgs, message, bodyBuilder);

        message.Body = bodyBuilder.ToMessageBody();

        await SendAsync(message);
    }

    private static void ProcessAdditionalArgs(AdditionalEmailSendingArgs? additionalEmailSendingArgs, MimeMessage message, BodyBuilder bodyBuilder)
    {
        if (additionalEmailSendingArgs?.Attachments != null)
        {
            foreach (var attachment in additionalEmailSendingArgs.Attachments)
            {
                bodyBuilder.Attachments.Add(attachment.Name, attachment.File);
            }
        }

        var cc = additionalEmailSendingArgs?.CC;
        if (cc is not null && cc.Count > 0)
        {
            foreach (var ccEmail in cc)
            {
                message.Cc.Add(new MailboxAddress("", ccEmail));
            }
        }
    }



    // email templates
    private readonly EmailTemplate orderConfirmationTemplate = new()
    {
        Subject = "Your Order Confirmation - KKBookstore"
    };

    private readonly EmailTemplate orderShippedTemplate = new()
    {
        Subject = "Your Order Has Been Shipped - KKBookstore"
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

        await SendAsync(message);
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

        await SendAsync(message);

    }

    public async Task SendOrderConfirmation(string email)
    {
        orderConfirmationTemplate.Body = EmailBodyHelper.BuildOrderConfirmationEmailBody();

        var message = CreateEmailMessage(
            orderConfirmationTemplate.Subject,
            orderConfirmationTemplate.Body,
            email
        );

        await SendAsync(message);
    }

    public async Task SendOrderShippedEmailAsync(string email)
    {
        orderShippedTemplate.Body = EmailBodyHelper.BuildOrderShippedEmailBody();

        var message = CreateEmailMessage(
            orderShippedTemplate.Subject,
            orderShippedTemplate.Body,
            email
        );

        await SendAsync(message);
    }

    public async Task SendPasswordResetLink(string email, string token)
    {
        var resetPasswordTemplate = new EmailTemplate
        {
            Subject = "Reset Your Password - KKBookstore",
            Body = "To reset your password, please click the following link: {resetLink}"
        };

        var message = CreateEmailMessage(
            resetPasswordTemplate.Subject,
            resetPasswordTemplate.Body,
            email,
            new Dictionary<string, string>()
            {
                { "{resetLink}", $"{_emailConfig.ResetPasswordLink}?email={email}&token={token}" }
            }
        );

        await SendAsync(message);
    }

    private MimeMessage CreateEmailMessage(string subject, string body, string toEmail, Dictionary<string, string>? placeholders = null)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailConsts.SenderName, _emailConfig.From));
        message.To.Add(new MailboxAddress(EmailConsts.ReceiverName, toEmail));
        message.Subject = subject;


        if (placeholders != null)
        {
            foreach (var placeholder in placeholders)
            {
                body = body.Replace(placeholder.Key, placeholder.Value);
            }
        }

        message.Body = new TextPart(EmailConsts.DefaultContentType) { Text = body };

        return message;
    }

    private async Task SendAsync(MimeMessage emailMessage)
    {
        var emailQueueClient = await GetQueueClient(StorageConsts.EmailQueueName);

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(StorageConsts.EmailBodyContainerName);
        var blobName = StorageConsts.EmailBodyFileNamePrefix + Guid.NewGuid().ToString();

        using (var stream = new MemoryStream())
        {
            emailMessage.WriteTo(stream);

            var blobClient = blobContainerClient.GetBlobClient(blobName);
            stream.Position = 0;
            await blobClient.UploadAsync(stream);
        }

        // base64 encode the blobname before send:
        var blobNameBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(blobName));


        await emailQueueClient.SendMessageAsync(blobNameBase64);
    }

    private async Task<QueueClient> GetQueueClient(string queueName)
    {
        var queueClient = _queueServiceClient.GetQueueClient(queueName);

        if (!queueClient.Exists())
        {
            await queueClient.CreateAsync();
        }

        return queueClient;
    }

    private static string SerializeMimeMessage(MimeMessage mailMessage)
    {
        using var stream = new MemoryStream();

        mailMessage.WriteTo(stream);

        var result = Convert.ToBase64String(stream.ToArray());
        return result;
    }
}
