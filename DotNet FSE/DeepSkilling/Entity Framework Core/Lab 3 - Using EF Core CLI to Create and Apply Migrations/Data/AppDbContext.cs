using Microsoft.EntityFrameworkCore;
using RetailMigration.Models;

namespace RetailMigration.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RetailMigrationDb;Trusted_Connection=True;");
    }
}
