# 5. WebApi_Handson — CORS and JWT Authentication

## Objectives

- Explain CORS and how to enable it for local access
- Demonstrate JWT authentication and role-based authorization
- Show how to generate JWT from an `AuthController` and use it in Postman

## Quick start

```powershell
cd "DeepSkilling\WebApi\5. WebApi_Handson"
dotnet restore
dotnet run --project WebApiJwtCors.csproj
```

- Swagger: `https://localhost:{port}/swagger`
- Generate token: `GET /api/Auth/token?userId=1&role=Admin`
- Use returned token in Postman: set header `Authorization: Bearer <token>`

Notes:
- CORS policy `LocalAllow` allows localhost origins; adjust in `Program.cs` as needed.
- The `EmployeeController` is decorated with `[Authorize(Roles = "POC")]`. Change to `[Authorize(Roles = "POC,Admin")]` to allow `Admin` role.
- Token expires in 10 minutes by default in `AuthController.GenerateJSONWebToken`.
