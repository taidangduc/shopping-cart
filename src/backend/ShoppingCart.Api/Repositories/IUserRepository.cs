using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
}