using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Api.DTOs;

public class TokenRequest
{
    [Required, EmailAddress, StringLength(255)]
    public string Email { get; set; }
    [Required, StringLength(128, MinimumLength = 1)]
    public string Password { get; set; }
}