using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Products.CreateProduct;

public record CreateProductCommand : IRequest<Result<CreateProductResponse>>
{

}
