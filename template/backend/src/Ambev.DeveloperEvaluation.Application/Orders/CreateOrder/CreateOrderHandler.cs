using Ambev.DeveloperEvaluation.Application.Validator;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CreateOrderHandler> _logger;
    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ILogger<CreateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new OrderValidator();
        var validationResult = validator.Validate(command);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var order = new Order
        {
            CustomerId = command.CustomerId,
            ShopId = command.ShopId
        };
        
        foreach (var orderItem in command.OrderItems)
        {
            var validateOrderItems = orderItem.ValidateOrderItems();
            if (!validateOrderItems.IsValid)
                throw new ValidationException(validateOrderItems.Errors);
            
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId, cancellationToken);
            if (product is null) throw new ArgumentNullException($"Product with ID '{orderItem.ProductId}' not found");
            
            var newOrderItem = new OrderItem(
                order.Id,
                orderItem.Quantity,
                product.Id,
                product.Price);
            
            order.AddOrderItem(newOrderItem);
        }
        
        var createOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        
        var result = new CreateOrderResult
        {
            OrderId = createOrder.Id,
            CustomerId = order.CustomerId, 
            ShopId = order.ShopId,
            OrderItems = order.OrderItems.Select(x => new CreateOrderItemResult
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
            }),
            Total = order.Total,
        };
        
        _logger.LogInformation($"Order with number '{createOrder.Number}' has been created");
        return result;
    }
}