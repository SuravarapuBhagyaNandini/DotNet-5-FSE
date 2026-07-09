# Lab 4: Inserting Initial Data into the Database

## Scenario
The store manager wants to add initial product categories and products to the system.

## Objective
Use EF Core to insert records using `AddAsync` and `SaveChangesAsync`.

## Steps
1. Insert Data in `Program.cs`:
```csharp
using var context = new AppDbContext();
var electronics = new Category { Name = "Electronics" };
var groceries = new Category { Name = "Groceries" };
await context.Categories.AddRangeAsync(electronics, groceries);
var product1 = new Product { Name = "Laptop", Price = 75000m, Category = electronics };
var product2 = new Product { Name = "Rice Bag", Price = 1200m, Category = groceries };
await context.Products.AddRangeAsync(product1, product2);
await context.SaveChangesAsync();
```
2. Run the App:
   - `dotnet run`
3. Verify in SQL Server:
   - Check that the data is inserted correctly into `Products` and `Categories`.

## Project Structure
- `RetailSeed.csproj`
- `Program.cs`
- `Models/`
  - `Category.cs`
  - `Product.cs`
- `Data/`
  - `AppDbContext.cs`

## Notes
- `AddRangeAsync` adds multiple entities to the change tracker asynchronously.
- `SaveChangesAsync` commits the inserted rows to the database.
- You can verify the inserted data in SQL Server Management Studio or Azure Data Studio.
