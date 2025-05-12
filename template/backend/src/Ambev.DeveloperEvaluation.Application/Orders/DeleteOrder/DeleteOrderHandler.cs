using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

public class DeleteOrderHandler(IOrderRepository orderRepository)
    : IRequestHandler<DeleteOrderCommand, DeleteOrderReult>
{
    public async Task<DeleteOrderReult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        if(!command.OrderId.HasValue)
            throw new Exception("Invalid command");

        var order = await orderRepository.GetByIdAsync(command.OrderId.Value, cancellationToken);
        
        if(order == null)
            throw new InvalidOperationException($"Order with id: {command.OrderId} does not exist");
        
        var orderCancelled = await orderRepository.DeleteAsync(order);
        
        var result = new DeleteOrderReult
        {
            OrderId = orderCancelled.Id,
            Deleted = true
        };
        
        return result;
    }
}