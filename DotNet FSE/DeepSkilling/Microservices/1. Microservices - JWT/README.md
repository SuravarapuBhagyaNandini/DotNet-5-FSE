# 1. Microservices - JWT in Microservices

## Scenario
You are building a microservice that requires secure login. This exercise demonstrates how to implement JWT-based authentication and authorization in an ASP.NET Core Web API.

## What this sample includes
- A `User` model for login input
- A login endpoint that issues a JWT token
- A protected endpoint that requires the `[Authorize]` attribute
- JWT validation settings configured in `Program.cs`

## Run the application
1. Open the project folder.
2. Restore packages:
   `dotnet restore`
3. Run the API:
   `dotnet run`

## Test the API
### 1. Login
Send a POST request to:
`http://localhost:5000/api/auth/login`

Body:
```json
{
  "username": "admin",
  "password": "password"
}
```

Expected response:
```json
{
  "token": "<jwt-token>"
}
```

### 2. Access a protected endpoint
Send a GET request to:
`http://localhost:5000/api/profile`

Add the token in the `Authorization` header:
```http
Authorization: Bearer <jwt-token>
```

## Notes
This lab uses a simple hard-coded credential check for learning purposes. In a real application, you would validate credentials against a database or identity provider.
