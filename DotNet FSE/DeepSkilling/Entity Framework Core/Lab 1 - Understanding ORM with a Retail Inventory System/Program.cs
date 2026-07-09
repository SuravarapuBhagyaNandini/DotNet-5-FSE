using RetailInventory.Data;
using RetailInventory.Models;

Console.WriteLine("Retail Inventory ORM demonstration with EF Core 8.0");
Console.WriteLine("This app shows how C# classes map to SQL Server tables using DbContext.");

var context = new RetailInventoryContext();
Console.WriteLine($"DbContext type: {context.GetType().Name}");
Console.WriteLine("Mapped entities:");
Console.WriteLine(" - Product -> Products");
Console.WriteLine(" - Category -> Categories");
Console.WriteLine("Provider: Microsoft SQL Server (SqlServer)");
Console.WriteLine();
Console.WriteLine("Sample object graph:");

var category = new Category { Name = "Electronics" };
var product = new Product { Name = "Wireless Mouse", StockLevel = 120, Category = category };
Console.WriteLine($"Category: {category.Name}");
Console.WriteLine($"Product: {product.Name}");
Console.WriteLine($"Stock level: {product.StockLevel}");
Console.WriteLine($"Association: {product.Name} belongs to {product.Category?.Name}");
