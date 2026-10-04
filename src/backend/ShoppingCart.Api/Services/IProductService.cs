using ShoppingCart.Api.DTOs;

namespace ShoppingCart.Api.Services;

public interface IProductService
{
    Task<ProductResponse> GetByIdAsync(string id, CancellationToken ct = default);
    Task<PaginatedResponse<ProductResponse>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);
}