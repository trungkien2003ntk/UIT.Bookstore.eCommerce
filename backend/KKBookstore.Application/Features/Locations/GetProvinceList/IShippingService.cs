using KKBookstore.Domain.Models;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Interfaces;

public partial interface IShippingService
{
    Task<Result<GetProvinceResponse>> GetProvinceAsync(CancellationToken cancellationToken);
}

public record class GetProvinceResponse
{
    [JsonPropertyName("data")]
    public List<Province> Provinces { get; init; } = [];

    public sealed record Province
    {
        [JsonPropertyName("ProvinceId")]
        public int Id { get; init; }
        [JsonPropertyName("ProvinceName")]
        public string Name { get; init; }
    }
}