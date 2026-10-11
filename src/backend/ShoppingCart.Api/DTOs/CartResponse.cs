using ShoppingCart.Api.Models;

namespace ShoppingCart.Api.DTOs;

public class CartResponse
{
    public string Id { get; set; } = default!;
    public List<CartProduct> Items { get; set; } = new List<CartProduct>();
}