using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderItemRepository(DefaultContext context) : IOrderItemRepository
{
    public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
       return await context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync(cancellationToken);
    }
    
    public async Task<List<OrderItem>> UpdateAsync(List<OrderItem> orderItems, CancellationToken cancellationToken = default)
    {
        context.OrderItems.UpdateRange(orderItems);
        await context.SaveChangesAsync(cancellationToken);
        return orderItems;
    }
}