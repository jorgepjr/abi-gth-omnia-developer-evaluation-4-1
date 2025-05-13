using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;

public class CancelOrderHandler(IOrderRepository orderRepository, ILogger<CancelOrderHandler> logger)
    : IRequestHandler<CancelOrderCommand, CancelOrderReult>
{
    public async Task<CancelOrderReult> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        if(!command.OrderId.HasValue)
            throw new Exception("Invalid command");

        var order = await orderRepository.GetByIdAsync(command.OrderId.Value, cancellationToken);
        
        if(order == null)
            throw new InvalidOperationException($"Order with id: {command.OrderId} does not exist");
        
        order.Cancel();
        var orderCancelled = await orderRepository.UpdateAsync(order, cancellationToken);
        
        var result = new CancelOrderReult
        {
            Cancelled = orderCancelled.Cancelled
        };
        
        logger.LogInformation($"Order cancelled with id: {order.Id}");
        
        return result;
    }
}