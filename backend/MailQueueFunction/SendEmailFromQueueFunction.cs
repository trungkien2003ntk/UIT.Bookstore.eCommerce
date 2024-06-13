using Azure.Storage.Blobs;
using MailKit.Net.Smtp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace MailQueueFunction;

public class SendEmailFromQueueFunction(
    ILogger<SendEmailFromQueueFunction> logger
)
{
    private readonly ILogger<SendEmailFromQueueFunction> _logger = logger;

    [Function(nameof(SendEmailFromQueueFunction))]
    public async Task Run(
        [QueueTrigger("mail-trigger", Connection = "StorageConnectionString:queue")] string mailBodyBlobName)
    {
        try
        {
            // queue message is blob uri of the email message
            var blobConnectionString = GetEnvironmentVariable("StorageConnectionString:blob");
            var blobClient = new BlobClient(blobConnectionString, "mail-body", mailBodyBlobName);

            MemoryStream stream = new();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0; // Reset stream position
            var mailMessage = MimeMessage.Load(stream);

            MimeEntity htmlBody = new TextPart("html") { Text = mailMessage.GetTextBody(MimeKit.Text.TextFormat.Plain) };

            var mailMessageHtml = new MimeMessage(mailMessage.From.AsEnumerable(), mailMessage.To.AsEnumerable(), mailMessage.Subject, htmlBody);

            var server = GetEnvironmentVariable("SMTP_SERVER");
            var port = int.Parse(GetEnvironmentVariable("SMTP_PORT"));
            var username = GetEnvironmentVariable("SMTP_USERNAME");
            var password = GetEnvironmentVariable("SMTP_PASSWORD");


            // 2. Send Email using SMTP Client
            using var client = new SmtpClient() ;
            await client.ConnectAsync(server, port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(username, password);
            
            await client.SendAsync(mailMessageHtml);

            _logger.LogInformation("Sent email from queue: {Subject}", mailMessage.Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing queue message");
        }
    }



    private static string GetEmailFromMailMessageEmail(InternetAddressList internetAddress)
    {
        string emailString = internetAddress.ToString();
        int startIndex = emailString.IndexOf('<') + 1; // Find the position after the opening bracket
        int endIndex = emailString.IndexOf('>');        // Find the position of the closing bracket
        string emailAddress = emailString[startIndex..endIndex];

        return emailAddress;
    }
    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? "";
    }
}

