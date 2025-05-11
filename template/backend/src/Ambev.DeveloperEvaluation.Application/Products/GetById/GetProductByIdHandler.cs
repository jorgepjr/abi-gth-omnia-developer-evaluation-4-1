using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetById;

public class GetProductByIdHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdCommand, GetProductByReult>
{
    public async Task<GetProductByReult> Handle(GetProductByIdCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken);
        
        var result = new GetProductByReult
        {
            ProductId = product!.Id,
            Name = product.Name,
            Price = product.Price,
        };
        
        return result;
    }
}