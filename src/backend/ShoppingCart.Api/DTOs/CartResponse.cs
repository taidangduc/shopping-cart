using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.DTOs;

public class CartResponse
{
    public string Id { get; set; } = default!;
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}