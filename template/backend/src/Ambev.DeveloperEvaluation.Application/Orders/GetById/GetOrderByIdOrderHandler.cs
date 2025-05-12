using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetById;

public class GetOrderByIdOrderHandler : IRequestHandler<GetOrderbyIdCommand, GetOrderByReult>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderByReult> Handle(GetOrderbyIdCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);
        
        var result = new GetOrderByReult
        {
            OrderId = order!.Id,
            CustomerId = order.CustomerId, 
            ShopId = order.ShopId,
            OrderItems = order.OrderItems.Select(x => new CreateOrderItemResult
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Discount = x.ItemWithDiscount,
                FinalPrice = x.FinalPrice,
                Cancelled = x.Cancelled,
            }),
            OrderNumber = order.Number,
            Total = order.Total,
            Date = order.CreateAt,
            Customer = order.Customer.Name,
            Store = order.Shop.TradeName,
        };
        
        return result;
    }
}