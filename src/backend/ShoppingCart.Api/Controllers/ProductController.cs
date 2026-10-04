namespace ShoppingCart.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ProductResponse>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        return Ok(await service.GetAllAsync(page, pageSize, ct));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<ProductResponse>> GetById(string id, CancellationToken ct = default)
    {
        var product = await service.GetByIdAsync(id, ct);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}