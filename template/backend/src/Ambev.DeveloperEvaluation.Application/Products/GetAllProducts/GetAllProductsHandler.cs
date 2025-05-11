using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsCommand, GetAllProductsReult>
{
    public async Task<GetAllProductsReult> Handle(GetAllProductsCommand command, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(
            command.PageSize, command.PageNumber, cancellationToken);
        
        return new GetAllProductsReult
        {
           Products =  products,
          TotalCount =  products.TotalCount,
          CurrentPage =  products.CurrentPage,
          TotalPages  = products.TotalPages,
        } ;
    }
}