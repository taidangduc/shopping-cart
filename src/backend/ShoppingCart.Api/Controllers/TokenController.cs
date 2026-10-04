namespace ShoppingCart.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class TokenController(IUserService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Post([FromBody] TokenRequest request, CancellationToken ct = default)
    {
        var token = await service.CreateTokenAsync(request, ct);
        if (token is null)
        {
            return BadRequest();
        }
        return Ok(token);
    }
}