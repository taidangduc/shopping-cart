using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.Data;

public sealed class AppDbContextSeed
{
    private readonly AppDbContext _dbContext;
    private readonly IEnumerable<Product> _mockProducts =
    [
        new Product { Id = "product-001", Name = "Wireless Headphones", Price = 79.99m, Stock = 25 },
        new Product { Id = "product-002", Name = "Mechanical Keyboard", Price = 109.50m, Stock = 18 },
        new Product { Id = "product-003", Name = "USB-C Hub", Price = 34.99m, Stock = 40 },
        new Product { Id = "product-004", Name = "Laptop Stand", Price = 45.00m, Stock = 30 },
        new Product { Id = "product-005", Name = "Wireless Mouse", Price = 29.99m, Stock = 50 }
    ];
    private readonly IEnumerable<User> _mockUsers;

    public AppDbContextSeed(
        AppDbContext dbContext,
        IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;

        var demoUser = new User
        {
            Id = "user-demo",
            Username = "demo",
            Email = "demo@shoppingcart.local",
            PasswordHash = string.Empty
        };
        demoUser.PasswordHash = passwordHasher.HashPassword(demoUser, "Password123!");
        _mockUsers = [demoUser];
    }

    public async Task SeedAllAsync(CancellationToken cancellationToken = default)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await SeedProductsAsync(cancellationToken);
                await SeedUsersAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }
    }

    private async Task SeedProductsAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        _dbContext.Products.AddRange(_mockProducts);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedUsersAsync(CancellationToken cancellationToken)
    {
        if (await _dbContext.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        _dbContext.Users.AddRange(_mockUsers);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
