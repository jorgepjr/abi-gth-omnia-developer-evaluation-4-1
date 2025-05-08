using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly DefaultContext _context;

    public ShopRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Shop> CreateAsync(Shop shop, CancellationToken cancellationToken = default)
    {
        await _context.Shops.AddAsync(shop, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return shop;
    }
}