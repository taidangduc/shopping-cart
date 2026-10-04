using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Services;

namespace ShoppingCart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(ICartService service, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<CartResponse>> GetCart(CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Unauthorized();
        }

        var cart = await service.GetByIdAsync(currentUser.UserId, ct);
        if (cart is null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CartResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<CartResponse>> CreateOrUpdateCart(List<CartItemRequest> Items, CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Unauthorized();
        }

        var cart = await service.CreateOrUpdateAsync(currentUser.UserId, Items, ct);
        return Ok(cart);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<ActionResult> DeleteCart(CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Unauthorized();
        }

        await service.DeleteAsync(currentUser.UserId, ct);
        return NoContent();
    }
}
