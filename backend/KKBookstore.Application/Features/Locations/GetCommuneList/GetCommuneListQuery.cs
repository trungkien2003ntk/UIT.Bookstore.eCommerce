using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Locations.GetCommuneList;

public class GetCommuneListQuery : IRequest<Result<GetCommuneListResponse>>
{
    public int DistrictId { get; set; }
}

public class GetCommuneListHandler(
    IShippingService shippingService
) : IRequestHandler<GetCommuneListQuery, Result<GetCommuneListResponse>>
{
    private readonly IShippingService _shippingService = shippingService;

    public async Task<Result<GetCommuneListResponse>> Handle(GetCommuneListQuery request, CancellationToken cancellationToken)
    {
        var result = await _shippingService.GetCommuneAsync(request.DistrictId, cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<GetCommuneListResponse>(result.Error);
        }

        var communes = result.Value.Communes;

        var response = new GetCommuneListResponse
        {
            Items = communes.Select(c => new GetCommuneListResponse.CommuneDto
            {
                Code = c.Code,
                Name = c.Name
            }).ToList()
        };

        return response;
    }
}