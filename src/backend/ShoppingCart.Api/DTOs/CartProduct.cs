namespace ShoppingCart.Api.DTOs;

public class CartProduct
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}