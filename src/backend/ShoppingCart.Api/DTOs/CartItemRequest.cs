namespace ShoppingCart.Api.DTOs;
using System.ComponentModel.DataAnnotations;
public class CartItemRequest
{
    [Required]
    public string Id { get; set; }
    [Required, Range(1, 100)]
    public int Quantity { get; set; }
}