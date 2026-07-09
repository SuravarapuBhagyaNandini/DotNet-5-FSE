using Microsoft.EntityFrameworkCore;
using RetailSeed.Models;

namespace RetailSeed.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RetailSeedDb;Trusted_Connection=True;");
    }
}
