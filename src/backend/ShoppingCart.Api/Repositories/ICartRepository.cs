namespace ShoppingCart.Api.Repositories;

using ShoppingCart.Api.Models;

public interface ICartRepository
{
    /// <summary>
    /// Get the cart for the specified user by ID.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Cart model or null if not found</returns>
    Task<Cart?> GetByIdAsync(string id, CancellationToken ct = default);
    
    /// <summary>
    /// Create or update a cart for the specified user.
    /// </summary>
    /// <param name="cart">Cart model</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Updated cart model or null if the operation failed</returns>
    Task<Cart?> CreateOrUpdateAsync(Cart cart, CancellationToken ct = default);

    /// <summary>
    /// Delete the cart for the specified user by ID.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>True if the cart was deleted successfully, otherwise false</returns>
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}