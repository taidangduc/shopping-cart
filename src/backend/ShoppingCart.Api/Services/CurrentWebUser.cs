using System.Security.Claims;

namespace ShoppingCart.Api.Services;

public class CurrentWebUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentWebUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId
    {
        get
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            var value = principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? principal?.FindFirstValue("sub");
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            throw new Exception("User ID claim not found or invalid.");
        }
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}