namespace KKBookstore.Application.Features.Locations.GetProvinceList;

public record GetProvinceListResponse
{
    public List<ProvinceDto> Items { get; init; } = [];

    public sealed record ProvinceDto
    {
        public int Id { get; init; }
        public string Label { get; init; }
    }
}