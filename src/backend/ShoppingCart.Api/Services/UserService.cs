namespace ShoppingCart.Api.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Api.DTOs;
using ShoppingCart.Api.Models;
using ShoppingCart.Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

public class UserService(
    IUserRepository repository,
    IConfiguration configuration,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    private UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username
        };
    }

    public async Task<UserResponse> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return ToResponse(await repository.GetByIdAsync(id, ct) ?? throw new InvalidOperationException("User not found."));
    }

    public Task<UserResponse> GetProfileAsync(string id, CancellationToken ct = default)
    {
        return GetByIdAsync(id, ct);
    }

    public async Task<TokenResponse> CreateTokenAsync(TokenRequest request, CancellationToken ct = default)
    {
        var user = await repository.GetByEmailAsync(request.Email.Trim().ToLowerInvariant(), ct);
        if (user is null)
        {
            throw new InvalidCredentialException("Invalid email or password.");
        }

        var verify = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verify == PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialException("Invalid email or password.");
        }

        var issueAt = DateTime.UtcNow;
        var lifetimeMinutes = Math.Max(1, configuration.GetValue<int>("JWT_TOKEN_LIFETIME_MINUTES"));
        var expiresIn = lifetimeMinutes * 60;
        var secret = configuration["JWT_SECRET_KEY"] ?? "long-time-no-see-32-bytes-long";

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)issueAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            }),
            Expires = issueAt.AddMinutes(lifetimeMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(descriptor)),
            TokenType = "Bearer",
            ExpiresIn = expiresIn
        };
    }
}