using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Repositories;

namespace ShoppingCart.Api.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    
    private static ProductResponse ToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    private static PaginatedResponse<ProductResponse> ToPaginated(IEnumerable<Product> items, int total, int page, int pageSize)
    {
        return new PaginatedResponse<ProductResponse>
        {
            Items = items.Select(ToResponse).ToList(),
            Total = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResponse<ProductResponse>> GetAllAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var (items, total) = await repository.GetAllAsync(page, pageSize, ct);
        return ToPaginated(items, total, page, pageSize);
    }

    public async Task<ProductResponse> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return ToResponse(await repository.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException($"Product with id '{id}' not found."));
    }
}