using KKBookstore.Domain.Models;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Interfaces;

public partial interface IShippingService
{
    Task<Result<GetDistrictResponse>> GetDistrictAsync(int provinceId, CancellationToken cancellationToken);
}

public record class GetDistrictResponse
{
    [JsonPropertyName("data")]
    public List<District> Districts { get; init; } = [];

    public sealed record District
    {
        [JsonPropertyName("DistrictId")]
        public int Id { get; init; }
        [JsonPropertyName("DistrictName")]
        public string Name { get; init; }
    }
}