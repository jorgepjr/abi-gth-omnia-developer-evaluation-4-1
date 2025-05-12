using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders;

public class GetAllOrdersHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersCommand, GetAllOrdersReult>
{
    public async Task<GetAllOrdersReult> Handle(GetAllOrdersCommand command, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(
            command.PageSize, command.PageNumber, cancellationToken);
        
        return new GetAllOrdersReult
        { 
            Orders =  orders,
            TotalCount =  orders.TotalCount,
            CurrentPage =  orders.CurrentPage,
            TotalPages  = orders.TotalPages,
        } ;
    }
}