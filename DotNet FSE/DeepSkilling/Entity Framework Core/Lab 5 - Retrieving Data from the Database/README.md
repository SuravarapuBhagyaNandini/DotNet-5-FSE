# Lab 5: Retrieving Data from the Database

## Scenario
The store wants to display product details on the dashboard.

## Objective
Use `Find`, `FirstOrDefault`, and `ToListAsync` to retrieve data.

## Steps
1. Retrieve All Products:
```csharp
var products = await context.Products.ToListAsync();
foreach (var p in products)
    Console.WriteLine($"{p.Name} - ₹{p.Price}");
```
2. Find by ID:
```csharp
var product = await context.Products.FindAsync(1);
Console.WriteLine($"Found: {product?.Name}");
```
3. FirstOrDefault with Condition:
```csharp
var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
Console.WriteLine($"Expensive: {expensive?.Name}");
```

## Notes
- `ToListAsync` retrieves all records asynchronously.
- `FindAsync` uses the primary key lookup for efficient retrieval.
- `FirstOrDefaultAsync` returns the first record matching the predicate or `null`.

## Project Structure
- `RetailQuery.csproj`
- `Program.cs`
- `Models/`
  - `Product.cs`
  - `Category.cs`
- `Data/`
  - `AppDbContext.cs`
