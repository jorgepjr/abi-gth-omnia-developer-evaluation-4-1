using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderItemRepository(DefaultContext context) : IOrderItemRepository
{
    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
       return await context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync(cancellationToken);
    }
}