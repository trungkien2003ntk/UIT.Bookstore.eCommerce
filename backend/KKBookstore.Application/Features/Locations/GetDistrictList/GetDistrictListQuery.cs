using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Locations.GetDistrictList;

public record GetDistrictListQuery : IRequest<Result<GetDistrictListResponse>>
{
    public int ProvinceId { get; init; }
}

public class GetDistrictListHandler(
    IShippingService shippingService    
) : IRequestHandler<GetDistrictListQuery, Result<GetDistrictListResponse>>
{
    private readonly IShippingService _shippingService = shippingService;

    public async Task<Result<GetDistrictListResponse>> Handle(GetDistrictListQuery request, CancellationToken cancellationToken)
    {
        var result = await _shippingService.GetDistrictAsync(request.ProvinceId, cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<GetDistrictListResponse>(result.Error);
        }

        var districts = result.Value.Districts;

        var response = new GetDistrictListResponse
        {
            Items = districts.Select(d => new GetDistrictListResponse.DistrictDto
            {
                Id = d.Id,
                Label = d.Name
            }).ToList()
        };

        return response;
    }
}
