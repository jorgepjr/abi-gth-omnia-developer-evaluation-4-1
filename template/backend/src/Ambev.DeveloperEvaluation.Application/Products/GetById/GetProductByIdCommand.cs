using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetById;

public class GetProductByIdCommand : IRequest<GetProductByReult>
{
    public Guid ProductId { get; set; }
}