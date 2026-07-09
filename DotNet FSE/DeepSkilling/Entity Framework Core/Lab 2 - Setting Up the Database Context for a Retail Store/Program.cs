using RetailStore.Data;
using RetailStore.Models;

Console.WriteLine("Lab 2: Setting up the EF Core database context for a retail store.");
Console.WriteLine("This example configures AppDbContext to connect to SQL Server.");

using var context = new AppDbContext();
Console.WriteLine($"DbContext type: {context.GetType().Name}");
Console.WriteLine("Entities configured:");
Console.WriteLine(" - Product -> Products");
Console.WriteLine(" - Category -> Categories");
Console.WriteLine();
Console.WriteLine("Example entity definitions:");

var category = new Category { Name = "Office Supplies" };
var product = new Product { Name = "Printer Paper", Price = 15.99m, Category = category };
Console.WriteLine($"Category: {category.Name}");
Console.WriteLine($"Product: {product.Name}");
Console.WriteLine($"Price: {product.Price:C}");
Console.WriteLine($"Category association: {product.Category?.Name}");
