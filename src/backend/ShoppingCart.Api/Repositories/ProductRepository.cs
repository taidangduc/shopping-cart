using ShoppingCart.Api.Data;
using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
    }
}