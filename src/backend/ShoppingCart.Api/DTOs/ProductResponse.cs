namespace ShoppingCart.Api.DTOs;

public class ProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Stock { get; set; }
}