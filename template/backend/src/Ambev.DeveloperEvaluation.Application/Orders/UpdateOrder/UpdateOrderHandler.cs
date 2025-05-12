using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    public UpdateOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
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
        
        return result;
    }
}