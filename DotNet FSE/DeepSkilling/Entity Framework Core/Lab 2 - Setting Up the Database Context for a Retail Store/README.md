# Lab 2: Setting Up the Database Context for a Retail Store

## Scenario
The retail store wants to store product and category data in SQL Server.

## Objective
Configure DbContext and connect to SQL Server.

## Steps
1. Create Models:
   - `Category` with `Id`, `Name`, and `Products`.
   - `Product` with `Id`, `Name`, `Price`, `CategoryId`, and `Category`.
2. Create `AppDbContext`:
   - Configure `DbSet<Product>` and `DbSet<Category>`.
   - Override `OnConfiguring` to connect with SQL Server.
3. Add Connection String in `appsettings.json` (optional for ASP.NET Core).

## Project Structure
- `RetailStore.csproj`
- `Program.cs`
- `Models/`
  - `Category.cs`
  - `Product.cs`
- `Data/`
  - `AppDbContext.cs`

## Notes
- `AppDbContext` is the central EF Core configuration point for the database.
- `DbSet<T>` properties map entity collections to database tables.
- In ASP.NET Core, the connection string is typically stored in `appsettings.json` and registered via DI.
