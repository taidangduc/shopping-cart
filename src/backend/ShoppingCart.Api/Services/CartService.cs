using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Repositories;

namespace ShoppingCart.Api.Services;

public class CartService(ICartRepository cartRepository, IProductRepository productRepository) : ICartService
{
    private CartResponse ToResponse(string cartId, List<CartProduct> items)
    {
        return new CartResponse
        {
            Id = cartId,
            Items = items
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
        var cart = await cartRepository.GetByIdAsync(id, ct) ?? throw new InvalidOperationException($"Cart with ID '{id}' not found.");
        return await ToPostProcessAsync(cart, ct);
    }

    public async Task<CartResponse?> CreateOrUpdateAsync(string id, List<CartItemRequest> items, CancellationToken ct = default)
    {
        var cart = await cartRepository.CreateOrUpdateAsync(ToModel(id, items), ct) ?? throw new InvalidOperationException($"Failed to create or update cart for ID '{id}'.");
        return await ToPostProcessAsync(cart, ct);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        return cartRepository.DeleteAsync(id, ct);
    }

    public async Task<CartResponse?> ToPostProcessAsync(Cart cart, CancellationToken ct = default)
    {
        if (cart.Items.Count == 0)
        {
            return null;
        }
        
        var productIds = cart.Items.Select(item => item.Id!).Distinct().ToList();

        var products = await productRepository.GetByIdsAsync(productIds, ct); 

        if (products?.Count is null or 0)
        {
            return null;
        }

        var cartProduct = new List<CartProduct>();

        var productLookup = products.ToDictionary(p => p.Id);

        foreach (var item in cart.Items)
        {
            if (!productLookup.TryGetValue(item.Id!, out var product))
            {
                continue;
            }

            cartProduct.Add(new CartProduct
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            });
        }
        return ToResponse(cart.Id, cartProduct);
    }
}