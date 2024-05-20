using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Orders.Query.GetOrderDetail;

public record GetOrderDetailQuery(int Id) : IRequest<Result<OrderDetailDto>>;
