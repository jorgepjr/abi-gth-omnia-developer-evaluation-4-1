using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await  _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
    }

    public async Task<PaginatedList<Product>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .AsNoTracking()
            .OrderBy(x => x.Name);
        
        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        var count = await query.CountAsync(cancellationToken);
        
        return new PaginatedList<Product>(
            products,
            count,
            pageNumber,
            pageSize);
    }
    
    public async Task<Product> DeleteAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x=>x.Id ==  productId);
        
        _context.Products.Remove(product!);
        await _context.SaveChangesAsync();

        return product!;
    }
}