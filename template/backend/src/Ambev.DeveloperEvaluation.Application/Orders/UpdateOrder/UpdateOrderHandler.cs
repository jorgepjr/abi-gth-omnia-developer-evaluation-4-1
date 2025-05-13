using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<UpdateOrderHandler> _logger;

    public UpdateOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ILogger<UpdateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderDb = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        
        if(orderDb is null)
            throw new ValidationException("Order not found");
        
        foreach (var orderItemDb in orderDb.OrderItems)
        {
            var orderItemUpdate = command.OrderItems.FirstOrDefault(x=>x.OrderItemId == orderItemDb.Id);
            
            if (orderItemUpdate != null)
            {
                var product = await _productRepository.GetByIdAsync(orderItemUpdate.ProductId, cancellationToken);
                
                if (product is null) throw new ArgumentNullException($"Product with ID '{orderItemUpdate.ProductId}' not found");
                
                orderItemDb.UpdateItem(orderItemUpdate.ProductId, orderItemUpdate.Quantity);
            }
        }
        
        var updatedOrder = await _orderRepository.UpdateAsync(orderDb, cancellationToken);
        
        var result = new UpdateOrderResult
        {
            OrderId = updatedOrder.Id,
            CustomerId = orderDb.CustomerId, 
            ShopId = orderDb.ShopId,
            OrderItems = orderDb.OrderItems.Select(x => new UpdateOrderItemResult
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
            }),
            Total = orderDb.Total,
        };
        
        _logger.LogInformation($"Order with Id '{updatedOrder.Id}' has been updated");
        
        return result;
    }
}