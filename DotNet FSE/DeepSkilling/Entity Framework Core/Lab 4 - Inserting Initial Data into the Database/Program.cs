using RetailSeed.Data;
using RetailSeed.Models;

Console.WriteLine("Lab 4: Inserting initial data into the database using EF Core.");

await using var context = new AppDbContext();

var electronics = new Category { Name = "Electronics" };
var groceries = new Category { Name = "Groceries" };

await context.Categories.AddRangeAsync(electronics, groceries);

var product1 = new Product { Name = "Laptop", Price = 75000m, Category = electronics };
var product2 = new Product { Name = "Rice Bag", Price = 1200m, Category = groceries };

await context.Products.AddRangeAsync(product1, product2);
await context.SaveChangesAsync();

Console.WriteLine("Initial data inserted successfully.");
Console.WriteLine("Verify the Products and Categories tables in SQL Server.");
