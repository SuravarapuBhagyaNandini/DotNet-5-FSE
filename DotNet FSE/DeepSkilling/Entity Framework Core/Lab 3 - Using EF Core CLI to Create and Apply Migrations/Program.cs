using RetailMigration.Data;
using RetailMigration.Models;

Console.WriteLine("Lab 3: Using EF Core CLI to create and apply migrations.");
Console.WriteLine("This lab demonstrates the commands needed to generate a migration and update the database.");

Console.WriteLine("Step 1: Install EF Core CLI if needed:");
Console.WriteLine("  dotnet tool install --global dotnet-ef");
Console.WriteLine();
Console.WriteLine("Step 2: Add an initial migration:");
Console.WriteLine("  dotnet ef migrations add InitialCreate");
Console.WriteLine();
Console.WriteLine("Step 3: Apply migration to create database:");
Console.WriteLine("  dotnet ef database update");
Console.WriteLine();
Console.WriteLine("The generated Migrations folder contains code for the database schema.");
Console.WriteLine("Verify Products and Categories tables in SQL Server Management Studio or Azure Data Studio.");

var context = new AppDbContext();
Console.WriteLine($"DbContext type: {context.GetType().Name}");
Console.WriteLine("Configured to use SQL Server with the connection string in AppDbContext.");
