using ShoppingCart.Api.DTOs;
namespace ShoppingCart.Api.Services;

public interface ICartService
{
    Task<CartResponse?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<CartResponse?> CreateOrUpdateAsync(string id, List<CartItemRequest> cart, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}