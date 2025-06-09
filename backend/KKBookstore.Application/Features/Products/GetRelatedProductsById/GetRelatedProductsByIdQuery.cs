using KKBookstore.Common.Models;
using KKBookstore.Features.Products.Models;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Products.GetRelatedProductsById;

public record GetRelatedProductsByIdQuery(int ProductId) : IRequest<Result<List<ProductSummary>>>;
