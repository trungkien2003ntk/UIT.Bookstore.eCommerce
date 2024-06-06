namespace KKBookstore.Application.Features.Locations.GetDistrictList;

public record GetDistrictListResponse
{
    public List<DistrictDto> Items { get; init; } = [];

    public sealed record DistrictDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}