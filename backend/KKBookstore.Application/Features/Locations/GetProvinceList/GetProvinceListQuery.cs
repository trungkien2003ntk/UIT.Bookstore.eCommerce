using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Locations.GetProvinceList;

public record GetProvinceListQuery : IRequest<Result<GetProvinceListResponse>>
{
}

public class GetProvinceListHandler(
    IShippingService shippingService
) : IRequestHandler<GetProvinceListQuery, Result<GetProvinceListResponse>>
{
    public async Task<Result<GetProvinceListResponse>> Handle(GetProvinceListQuery request, CancellationToken cancellationToken)
    {
        var getProvinceResult = await shippingService.GetProvinceAsync(cancellationToken);
        if (getProvinceResult.IsFailure)
        {
            return Result.Failure<GetProvinceListResponse>(getProvinceResult.Error);
        }
        var provinces = getProvinceResult.Value.Provinces;


        var response = new GetProvinceListResponse
        {
            Items = provinces.Select(p => new GetProvinceListResponse.ProvinceDto
            {
                Id = p.Id,
                Label = p.Name
            }).ToList()
        };

        return response;
    }
}
