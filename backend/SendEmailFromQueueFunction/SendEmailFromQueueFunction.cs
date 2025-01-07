using Azure.Storage.Blobs;
using MailKit.Net.Smtp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace SendEmailFromQueueFunction;

public class SendEmailFromQueueFunction
{
    private readonly ILogger<SendEmailFromQueueFunction> _logger;

    public SendEmailFromQueueFunction(ILogger<SendEmailFromQueueFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(SendEmailFromQueueFunction))]
    public async Task Run([QueueTrigger("email-metadata", Connection = "StorageConnectionString")] string message)
    {
        try
        {
            // queue message is blob uri of the email message
            var blobConnectionString = GetEnvironmentVariable("StorageConnectionString");
            var blobClient = new BlobClient(blobConnectionString, "email-bodies", message);
            _logger.LogInformation("Processing queue message: {Message}", message);
            _logger.LogInformation("Processing on blob: {BlobName}", blobClient.Name);
            _logger.LogInformation("Processing on container: {ContainerName}", blobClient.BlobContainerName);

            MemoryStream stream = new();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0; // Reset stream position
            var mailMessage = MimeMessage.Load(stream);

            MimeEntity htmlBody = new TextPart("html") { Text = mailMessage.GetTextBody(MimeKit.Text.TextFormat.Plain) };

            var mailMessageHtml = new MimeMessage(mailMessage.From.AsEnumerable(), mailMessage.To.AsEnumerable(), mailMessage.Subject, htmlBody);
            _logger.LogInformation("Processing email message: {Subject}", mailMessage.Subject);

            var server = GetEnvironmentVariable("SMTP_SERVER");
            _logger.LogInformation("SMTP Server: {Server}", server);

            var port = int.Parse(GetEnvironmentVariable("SMTP_PORT"));
            _logger.LogInformation("SMTP Port: {Port}", port);

            var username = GetEnvironmentVariable("SMTP_USERNAME");
            _logger.LogInformation("SMTP Username: {Username}", username);

            var password = GetEnvironmentVariable("SMTP_PASSWORD");

            // 2. Send Email using SMTP Client
            using var client = new SmtpClient();
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
