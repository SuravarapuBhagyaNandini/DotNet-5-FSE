# 4.WebApi_Handson — Web API CRUD operations (PUT/POST/DELETE)

## Objectives

- Demonstrate creation of action methods to perform create, update & delete operations.
- Use `[FromBody]` to bind request JSON to custom model classes.
- Validate `id` in PUT and return appropriate `BadRequest` messages.
- Test with Swagger UI or Postman.

## Project: WebApiCrud

Run:
```powershell
cd "DeepSkilling\WebApi\4.WebApi_Handson\WebApiCrud"
dotnet restore
dotnet run
```

## Endpoints:
- `GET /api/Employee` - list employees
- `GET /api/Employee/{id}` - get by id
- `POST /api/Employee` - create (JSON body)
- `PUT /api/Employee/{id}` - update (JSON body) — validates id <= 0 or missing
- `DELETE /api/Employee/{id}` - delete

Notes:
- PUT returns `BadRequest("Invalid employee id")` when `id <= 0` or `id` not found in the hardcoded list.
- On successful PUT, the updated `Employee` is returned in the response body.
