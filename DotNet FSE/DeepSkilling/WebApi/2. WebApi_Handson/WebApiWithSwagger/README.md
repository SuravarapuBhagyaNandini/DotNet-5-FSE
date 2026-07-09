# WebApiWithSwagger

This sample demonstrates adding Swagger (Swashbuckle) to an ASP.NET Core Web API and using Postman to call endpoints.

## Install Swashbuckle
From the project folder run:

```powershell
dotnet add package Swashbuckle.AspNetCore
```

## Run

```powershell
cd "DeepSkilling\WebApi\2.WebApi_Handson\WebApiWithSwagger"
dotnet restore
dotnet run
```

## Swagger
Open `https://localhost:{port}/swagger` to see the API listing and try endpoints from the browser UI.

## Endpoints (EmployeeController)
- `GET /api/Employee` - list employees
- `GET /api/Employee/{id}` - retrieve by id (named route `GetEmployee`)
- `POST /api/Employee` - create
- `PUT /api/Employee/{id}` - update
- `DELETE /api/Employee/{id}` - delete
- `GET /api/Employee/search?name=Alice` - search by name (demonstrates ActionName)

## Postman Steps
1. Create a new collection and add a request.
2. Set method (GET/POST/etc) and URL (e.g. `https://localhost:{port}/api/Employee`).
3. For POST/PUT, select `Body -> raw -> JSON` and provide JSON payload.
4. Inspect response body and status code in Postman.

## Route name example
To change the controller route to `Emp`, modify the controller attribute to:

```csharp
[Route("api/Emp")]
public class EmployeeController : ControllerBase { ... }
```

Then call `GET /api/Emp` from Postman or browser.
