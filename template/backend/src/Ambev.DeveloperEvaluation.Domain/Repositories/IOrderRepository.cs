using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Order> UpdateAsync(Order orderDb, CancellationToken cancellationToken);
    Task<Order> DeleteAsync(Order order);
    Task<PaginatedList<Order>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default);
}