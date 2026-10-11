# Shopping Cart API

## Overview

The API serves products and user profiles from SQL Server. It stores user carts in
Redis. JWT bearer authentication is used for protected routes.

## Tech stack

- .NET 8 and ASP.NET Core Web API
- Entity Framework Core 8 with SQL Server
- Redis with StackExchange.Redis
- JWT bearer authentication
- Swagger for development API documentation

## Main request flow

1. `Program.cs` loads settings, registers services, repositories, SQL Server,
   Redis, and authentication, then maps controllers.
2. A controller accepts the HTTP request and calls its service.
3. Product and user services call repositories backed by `AppDbContext` and
   SQL Server.
4. Cart services call the Redis-backed cart repository. Cart IDs come from the
   authenticated user's identity.
5. The token endpoint verifies a user's password hash and returns a JWT.

## Folder structure

| Folder or file | Purpose |
|---|---|
| [Controllers](../src/backend/ShoppingCart.Api/Controllers/) | HTTP routes for carts, products, users, and tokens |
| [DTOs](../src/backend/ShoppingCart.Api/DTOs/) | Request and response types |
| [Data](../src/backend/ShoppingCart.Api/Data/) | EF Core context and database migrations |
| [Models](../src/backend/ShoppingCart.Api/Models/) | Product, user, and cart data types |
| [Repositories](../src/backend/ShoppingCart.Api/Repositories/) | SQL Server and Redis data access |
| [Services](../src/backend/ShoppingCart.Api/Services/) | Business logic and current-user access |
| [Program.cs](../src/backend/ShoppingCart.Api/Program.cs) | Application setup and dependency registration |
| [appsettings.json](../src/backend/ShoppingCart.Api/appsettings.json) | Local database, Redis, and token settings |
| [dotnet-cli.txt](../src/backend/ShoppingCart.Api/dotnet-cli.txt) | EF Core migration commands |

## Initialization status

The initial EF Core migration was created and applied to the local `CartDb`
database. It creates the `Products` and `Users` tables. The product-list endpoint
was smoke-tested successfully after setup.

## Issues to address

### Security

- **High — SQL credentials in settings.** The default settings contain a SQL
  Server `sa` password. This is being retained for the demo; do not deploy this
  configuration. Rotate the password and use a deployment-managed secret and a
  least-privilege database account before production.
- **User data is publicly readable by ID.** `GET /api/User/{id}` has no
  authorization requirement and returns profile fields. Require authentication
  and restrict access to the user's own profile, or explicitly define an
  administrative policy.
- **JWT configuration does not match token issuance.** The token service signs
  tokens with a configured symmetric key, while bearer validation points to an
  authority at `https://localhost:5001`. The issued token also does not set the
  configured audience. As a result, tokens issued by this API may not work on
  protected routes. Configure issuance and validation to use the same signing
  key, issuer, and audience.
- **Demo signing key.** The configured JWT key is a placeholder, and the token
  service has a hard-coded fallback. Use a strong secret supplied outside
  source control; remove the fallback before production.

### Bugs and code warnings

- **Fixed during setup:** the user route used an unsupported `:string` route
  constraint, which caused routing to fail. It now uses the default string
  route parameter.
- **Build warnings:** the build succeeds, but reports 20 unique `CS8618`
  warnings. Non-nullable DTO and model properties need `required` declarations,
  valid defaults, or nullable types as appropriate.
