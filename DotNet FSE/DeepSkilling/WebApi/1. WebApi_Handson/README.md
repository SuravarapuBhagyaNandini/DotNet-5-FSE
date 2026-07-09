# WebApi_Handson

## Objectives

- Explain the concept of RESTful web service, Web API & Microservice
  - Features of REST architecture: Representational State Transfer, Stateless, Messages
  - Concept of Microservice
  - Difference between WebService & WebAPI — not restricted to XML responses

- Explain what is `HttpRequest` & `HttpResponse`

- List the types of Action Verbs
  - `HttpGet`, `HttpPost`, `HttpPut`, `HttpDelete` — meaning and how to declare as attributes for Web API

- List the types of HttpStatusCodes used in WebAPI
  - Examples: `Ok`, `InternalServerError`, `Unauthorized`, `BadRequest` (returned via action result types)

- Demonstrate creation of a simple WebAPI — Read/Write actions
  - Structure: Controller (inherits from `ControllerBase` / `ApiController`), Action verbs, Action methods

- Explain configuration files of WebAPI
  - `Program.cs` / `Startup.cs` with dependency injection, `appsettings.json`, `launchSettings.json`
  - In .NET Framework (4.x): `Route.config` & `WebAPI.config`

---

## First Web Api using .NET Core

1. Create a new Web API project (template) and enable creating controller with read/write actions.
2. Observe `ValuesController` with action methods for `GET`, `POST`, `PUT`, `DELETE`.
3. Run the app and verify the `GET` action returns the expected result.

Run example project in the `FirstWebApi` folder.
