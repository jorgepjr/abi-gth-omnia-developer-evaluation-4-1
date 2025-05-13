using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ILogger<UpdateOrderHandler> _logger;

    public UpdateOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ILogger<UpdateOrderHandler> logger, IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _logger = logger;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderDb = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

        if (orderDb is null)
            throw new ValidationException("Order not found");

        foreach (var orderItemCommand in command.OrderItems.ToList())
        {
            var isExistItem = await _orderItemRepository.GetByOrderIdAndProductIdAsync(orderDb.Id, orderItemCommand.ProductId);

            if (isExistItem is not null)
            {
                isExistItem.UpdateItem(orderItemCommand.ProductId, orderItemCommand.Quantity);
            }
            else
            {
                var product = await _productRepository.GetByIdAsync(orderItemCommand.ProductId, cancellationToken);
                if (product is null)
                    throw new ValidationException("Product not found");

                var newOrderItem = new OrderItem(
                    orderDb.Id,
                    orderItemCommand.Quantity,
                    orderItemCommand.ProductId,
                    product!.Price);

                orderDb.AddOrderItem(newOrderItem);
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
                Discount = x.ItemWithDiscount,
                FinalPrice = x.FinalPrice,
            }),
            Total = orderDb.Total,
        };

        _logger.LogInformation($"Order with Id '{updatedOrder.Id}' has been updated");

        return result;
    }
}