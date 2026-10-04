namespace ShoppingCart.Api.Repositories;

using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Data;
using ShoppingCart.Api.Models;

public class UserRepository(AppDbContext db) : BaseRepository<User>(db), IUserRepository
{
    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return Db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower(), ct);
    }
}