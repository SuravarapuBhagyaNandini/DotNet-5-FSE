using Microsoft.EntityFrameworkCore;
using RetailQuery.Data;
using RetailQuery.Models;

Console.WriteLine("Lab 5: Retrieving data from the database using EF Core.");

await using var context = new AppDbContext();

Console.WriteLine("Retrieving all products...");
var products = await context.Products.ToListAsync();
foreach (var p in products)
{
    Console.WriteLine($"{p.Name} - ₹{p.Price}");
}

Console.WriteLine();
Console.WriteLine("Finding product with ID = 1...");
var product = await context.Products.FindAsync(1);
Console.WriteLine($"Found: {product?.Name ?? "Not found"}");

Console.WriteLine();
Console.WriteLine("Finding expensive product with price > 50000...");
var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
Console.WriteLine($"Expensive: {expensive?.Name ?? "None"}");
