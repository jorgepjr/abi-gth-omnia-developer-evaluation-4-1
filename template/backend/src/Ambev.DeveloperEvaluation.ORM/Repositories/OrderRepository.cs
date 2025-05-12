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
    
    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }
    
    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(x=>x.Customer)
            .Include(x=>x.Shop)
            .Include(x=>x.OrderItems)
            .FirstOrDefaultAsync(x=>x.Id == orderId, cancellationToken);
        
        return order;
    }
    
    public async Task<Order> DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
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