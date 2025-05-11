using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsCommand : IRequest<GetAllProductsReult>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}