using ShoppingCart.Api.DTOs;

namespace ShoppingCart.Api.Services;

public interface IUserService
{
    Task<UserResponse> GetByIdAsync(string id, CancellationToken ct = default);
    Task<UserResponse> GetProfileAsync(string id, CancellationToken ct = default);
    Task<TokenResponse> CreateTokenAsync(TokenRequest request, CancellationToken ct = default);
}