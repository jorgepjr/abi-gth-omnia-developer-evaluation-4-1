using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetById;

public class GetOrderbyIdCommand : IRequest<GetOrderByReult>
{
    public Guid OrderId { get; set; }
}