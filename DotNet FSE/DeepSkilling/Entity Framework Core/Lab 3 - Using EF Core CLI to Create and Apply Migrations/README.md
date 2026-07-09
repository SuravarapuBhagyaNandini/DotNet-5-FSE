# Lab 3: Using EF Core CLI to Create and Apply Migrations

## Scenario
The retail store's database needs to be created based on the models you've defined. You’ll use EF Core CLI to generate and apply migrations.

## Objective
Learn how to use EF Core CLI to manage database schema changes.

## Steps
1. Install EF Core CLI (if not already):
   - `dotnet tool install --global dotnet-ef`
2. Create Initial Migration:
   - `dotnet ef migrations add InitialCreate`
   - This generates a `Migrations` folder with code that represents the schema.
3. Apply Migration to Create Database:
   - `dotnet ef database update`
4. Verify in SQL Server:
   - Open SQL Server Management Studio (SSMS) or Azure Data Studio and confirm that tables `Products` and `Categories` are created.

## Project Structure
- `RetailMigration.csproj`
- `Program.cs`
- `Models/`
  - `Product.cs`
  - `Category.cs`
- `Data/`
  - `AppDbContext.cs`

## Notes
- Run these commands from the project folder containing `RetailMigration.csproj`.
- `dotnet ef migrations add InitialCreate` creates the schema migration.
- `dotnet ef database update` applies the migration and creates the database.
- Verify the database using SSMS or Azure Data Studio.
