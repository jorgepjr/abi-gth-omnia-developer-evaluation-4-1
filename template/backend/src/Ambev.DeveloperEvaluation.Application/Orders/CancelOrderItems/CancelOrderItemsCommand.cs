using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItems;

public class CancelOrderItemsCommand : IRequest<List<CancelOrderItemsReult>>
{
    public Guid? OrderId { get; set; }
    public List<Guid?>? OrderItemsIds { get; set; } = null;
}