using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(command);
        
        foreach (var items in command.OrderItems)
        {
            var product = await _productRepository.GetById(items.ProductId);
            
            if (product is null) throw new ArgumentNullException();
            order.AddOrderItem(new OrderItem{OrderId = order.Id, ProductId = product.Id, UnitPrice = product.Price});
        }
        
        var createOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        
        var result = _mapper.Map<CreateOrderResult>(createOrder);
        return result;
    }
}