using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface IShopRepository
{
    Task<Shop> CreateAsync(Shop shop, CancellationToken cancellationToken = default);
}
