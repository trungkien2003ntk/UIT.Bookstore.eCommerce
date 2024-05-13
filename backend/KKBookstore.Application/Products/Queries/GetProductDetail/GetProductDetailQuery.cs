using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Products.Queries.GetProductDetail;

public record GetProductDetailQuery(int ProductId) : IRequest<Result<ProductDetailDto>>
{
}
