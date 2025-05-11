using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DefaultContext _context;

    public OrderRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await SetNextOrderNumber(order, cancellationToken);

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }
    
    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(x=>x.OrderItems)
            .FirstOrDefaultAsync(x=>x.Id == orderId, cancellationToken);
        
        return order;
    }
    
    private async Task SetNextOrderNumber(Order order, CancellationToken cancellationToken)
    {
        const long newNumber = 1;

        var lastOrderRegistred = await _context.Orders
            .OrderByDescending(x => x.Number).FirstOrDefaultAsync(cancellationToken);

        order.Number = lastOrderRegistred is null ? newNumber : lastOrderRegistred.Number + 1;
    }
}