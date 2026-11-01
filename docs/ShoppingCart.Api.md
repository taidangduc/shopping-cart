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