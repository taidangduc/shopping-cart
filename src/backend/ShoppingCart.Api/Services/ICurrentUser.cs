namespace ShoppingCart.Api.Services;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    string UserId { get; }
}
