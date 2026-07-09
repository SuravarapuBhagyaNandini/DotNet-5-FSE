# FirstWebApi (WebApi_Handson)

Minimal ASP.NET Core 8.0 Web API example with a `ValuesController` demonstrating CRUD.

Run:

```powershell
cd "DeepSkilling\WebApi\WebApi_Handson\FirstWebApi"
dotnet restore
dotnet run
```

Endpoints:

- `GET /api/values`
- `GET /api/values/{id}`
- `POST /api/values` (JSON)
- `PUT /api/values/{id}` (JSON)
- `DELETE /api/values/{id}`

Test using curl or Postman. Example:

```bash
curl https://localhost:5001/api/values -k
```
