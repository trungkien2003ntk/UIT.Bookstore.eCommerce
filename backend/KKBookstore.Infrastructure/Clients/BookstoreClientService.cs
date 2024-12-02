using KKBookstore.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KKBookstore.Infrastructure.Clients;

internal class BookstoreClientService(IConfiguration configuration) : IBookstoreClientService
{
    private readonly IConfiguration _configuration = configuration;

    public string ConstructPasswordResetLink(string token)
    {
        var baseUrl = _configuration["BookstoreClient:BaseUrl"];

        return $"{baseUrl}/reset-password?token={token}";
    }
}