using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;

public class CancelOrderCommand : IRequest<CancelOrderReult>
{
    public Guid? OrderId { get; set; }
}