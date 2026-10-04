namespace ShoppingCart.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetById(string id, CancellationToken ct = default)
    {
        var user = await service.GetByIdAsync(id, ct);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("me/profile")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetProfile(CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Unauthorized();
        }

        var user = await service.GetProfileAsync(currentUser.UserId, ct);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}