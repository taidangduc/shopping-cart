using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Repositories;

namespace ShoppingCart.Api.Services;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public CartResponse ToResponse(Cart cart)
    {
        return new CartResponse
        {
            Id = cart.Id,
            Items = cart.Items
        };
    }

    public Cart ToModel(string id, List<CartItemRequest> items)
    {
        return new Cart
        {
            Id = id,
            Items = items.Select(item => new CartItem { Id = item.Id, Quantity = item.Quantity }).ToList()
        };
    }

    public async Task<CartResponse?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return ToResponse(await cartRepository.GetByIdAsync(id, ct) ?? throw new InvalidOperationException($"Cart with ID '{id}' not found."));
    }

    public async Task<CartResponse?> CreateOrUpdateAsync(string id, List<CartItemRequest> items, CancellationToken ct = default)
    {
        return ToResponse(
            await cartRepository.CreateOrUpdateAsync(ToModel(id, items), ct) 
            ?? throw new InvalidOperationException($"Failed to create or update cart for ID '{id}'."));
    }

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        return cartRepository.DeleteAsync(id, ct);
    }
}