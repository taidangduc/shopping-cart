using ShoppingCart.Api.Data;
using ShoppingCart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Api.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<Product>> GetByIdsAsync(List<string> ids, CancellationToken ct = default)
    {
        return await Db.Products.Where(p => ids.Contains(p.Id)).ToListAsync(ct);
    }
}