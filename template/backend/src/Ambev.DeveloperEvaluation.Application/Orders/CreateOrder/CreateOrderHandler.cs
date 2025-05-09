using Ambev.DeveloperEvaluation.Application.Validator;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = command.CustomerId,
            ShopId = command.ShopId
        };
        
        foreach (var orderItem in command.OrderItems)
        {
            var validationResult = await ValidateOrderItems(cancellationToken, orderItem);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var product = await _productRepository.GetById(orderItem.ProductId);
            if (product is null) throw new ArgumentNullException($"Product with ID '{orderItem.ProductId}' not found");

            var newOrderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = orderItem.ProductId,
                UnitPrice = orderItem.UnitPrice,
                Quantity = orderItem.Quantity,
            };
            
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
        
        return result;
    }

    private static async Task<ValidationResult> ValidateOrderItems(CancellationToken cancellationToken, CreateOrderItemCommand orderItem)
    {
        var validator = new OrderItemValidator();
        var validationResult = await validator.ValidateAsync(orderItem, cancellationToken);
        return validationResult;
    }
}