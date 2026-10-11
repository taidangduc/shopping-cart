using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetByIdsAsync(List<string> ids, CancellationToken ct = default);
}