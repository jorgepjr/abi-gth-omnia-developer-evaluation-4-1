using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface IProductRepository
{
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<PaginatedList<Product>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default);
    Task<Product> DeleteAsync(Guid productId);
}
