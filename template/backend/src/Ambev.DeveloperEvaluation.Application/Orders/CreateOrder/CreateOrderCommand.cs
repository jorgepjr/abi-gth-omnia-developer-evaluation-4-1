using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    public Guid CustomerId { get; set; }
    public Guid ShopId { get; set; }
    public List<CreateOrderItemCommand> OrderItems { get; set; } = [];
}