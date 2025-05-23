using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItems;

public class CancelOrderItemsHandler : IRequestHandler<CancelOrderItemsCommand, List<CancelOrderItemsReult>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ILogger<CancelOrderHandler> _logger;
    public CancelOrderItemsHandler(IOrderItemRepository orderItemRepository, ILogger<CancelOrderHandler> logger)
    {
        _orderItemRepository = orderItemRepository;
        _logger = logger;
    }

    public async Task<List<CancelOrderItemsReult>> Handle(CancelOrderItemsCommand command, CancellationToken cancellationToken)
    {
        if(!command.OrderId.HasValue && command.OrderItemsIds is null)
            throw new Exception("orderId and orderItemsIds is null");

        var orderItems = await _orderItemRepository.GetOrderItemsByOrderId(command.OrderId!.Value, cancellationToken);
        
        if(orderItems == null)
            throw new InvalidOperationException($"Order with id: {command.OrderId} does not exist");
        
        if (command.OrderItemsIds!.Any())
        {
            foreach (var item in orderItems)
            {
                var itemCancel = command.OrderItemsIds!
                    .FirstOrDefault(itemId => itemId == item.Id);
        
                if(itemCancel != null)
                item.Cancel();
            }
        }
        var orderItemsCancelled = await _orderItemRepository.UpdateAsync(orderItems, cancellationToken);

       var result =  orderItemsCancelled.Select(x => new CancelOrderItemsReult
        {
            OrderItemId = x.Id,
            Cancelled = x.Cancelled
        }).ToList();
        
       _logger.LogInformation($"order items: {orderItemsCancelled.Count} cancelled");
       
        return result;
    }
}