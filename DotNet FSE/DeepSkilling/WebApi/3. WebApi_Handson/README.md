# 3.WebApi_Handson — Custom Model, Filters, Exception Handling

## Objectives

- Demonstrate creation of an action method returning a list of a custom class entity (`Employee`).
  - Model class `Employee` with `Department` and `Skill` nested types.
  - Use `[AllowAnonymous]` to allow anonymous access to the GET action.
  - Use `[HttpGet]` to declare the action verb.

- Explain use of `[FromBody]` to bind request JSON body to action parameters (used in POST/PUT).

- Demonstrate custom filters
  - `CustomAuthFilter` inherits `ActionFilterAttribute` and checks for `Authorization` header and `Bearer` token; returns `BadRequest` if invalid.
  - `CustomExceptionFilter` implements `IExceptionFilter`, logs exceptions to `logs/exceptions.txt`, and returns a 500 error body.

## Project: WebApiCustomModel

Run:

```powershell
cd "DeepSkilling\WebApi\3.WebApi_Handson"
dotnet restore
dotnet run --project WebApiCustomModel.csproj
```

## Test
- Open Swagger UI at `https://localhost:{port}/swagger` and call `GET /api/Employee`.
- To test exception handling, call `GET /api/Employee?throwError=true`.
- Use Postman to send `Authorization` header to test the `CustomAuthFilter`:
  - Header: `Authorization: Bearer sometoken`

Notes:
- `Microsoft.AspNetCore.Mvc.WebApiCompatShim` package is referenced for compatibility with exception handling helpers if needed.
