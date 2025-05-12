using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

public class DeleteOrderCommand : IRequest<DeleteOrderReult>
{
    public Guid? OrderId { get; set; }
}