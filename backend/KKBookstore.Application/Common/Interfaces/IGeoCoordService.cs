using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Users;

namespace KKBookstore.Common.Interfaces;

public interface IGeoCoordService
{
    Task<GeoCoordResult> GetCoordinatesAsync(Address address, CancellationToken cancellationToken = default);
    Task<GeoCoordResult> GetCoordinatesAsync(string fullAddress, CancellationToken cancellationToken = default);
}
