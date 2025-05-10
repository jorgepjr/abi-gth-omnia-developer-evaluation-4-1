using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
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
        var validateOrder = command.ValidateOrder();
        
        if (!validateOrder.IsValid)
            throw new ValidationException(validateOrder.Errors);
        
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
            
            var product = await _productRepository.GetById(orderItem.ProductId, cancellationToken);
            if (product is null) throw new ArgumentNullException($"Product with ID '{orderItem.ProductId}' not found");
            
            var newOrderItem = new OrderItem
            {
                OrderId = order.Id,
                Quantity = orderItem.Quantity,
                ProductId = product.Id,
                UnitPrice = product.Price,
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
}