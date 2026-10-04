using System.Text;
using System.Text.Json;
using ShoppingCart.Api.Models;
using StackExchange.Redis;

namespace ShoppingCart.Api.Repositories;

public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
{
    public async Task<Cart?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var database = redis.GetDatabase();
        var data = database.StringGet(GetCacheKey(id));

        return data.IsNullOrEmpty
            ? null
            : JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(data!));
    }

    public async Task<Cart?> CreateOrUpdateAsync(Cart cart, CancellationToken ct = default)
    {
        var database = redis.GetDatabase();
        var json = JsonSerializer.Serialize(cart);

        var result = await database.StringSetAsync(GetCacheKey(cart.Id), json);
        if (result)
        {
            return await GetByIdAsync(cart.Id, ct);
        }

        return null;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var database = redis.GetDatabase();
        return await database.KeyDeleteAsync(GetCacheKey(id));
    }

    private string GetCacheKey(string id)
    {
        return $"cart:{id}";
    }
}