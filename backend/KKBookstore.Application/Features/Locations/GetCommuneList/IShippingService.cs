using KKBookstore.Domain.Models;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Interfaces;

public partial interface IShippingService
{
    Task<Result<GetCommuneResponse>> GetCommuneAsync(int districtId, CancellationToken cancellationToken);
}

public record class GetCommuneResponse
{
    [JsonPropertyName("data")]
    public List<Commune> Communes { get; init; } = [];

    public sealed record Commune
    {
        [JsonPropertyName("WardCode")]
        public string Code { get; init; }
        [JsonPropertyName("WardName")]
        public string Name { get; init; }
    }
}
