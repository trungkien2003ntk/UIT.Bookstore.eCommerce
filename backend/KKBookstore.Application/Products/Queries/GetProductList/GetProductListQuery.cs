using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Products.Queries.GetProductList;

public record GetProductListQuery()
    : IRequest<Result<PaginatedResult<ProductGeneralDto>>>, ISortableQuery, IPaginatedQuery
{
    public string SortBy { get; init; } = "CreatedWhen";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    // other properties for filtering 
    public int? ProductTypeId { get; set; }
    public string? SearchTerm { get; set; }
}
