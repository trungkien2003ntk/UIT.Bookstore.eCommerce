using KKBookstore.Common.Models;
using KKBookstore.Features.Products.Models;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Products.GetRelatedProductsByImage;

public record GetRelatedProductsByImageQuery(string Base64Image) : IRequest<Result<List<ProductSummary>>>;
