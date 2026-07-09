using Microsoft.EntityFrameworkCore;
using RetailStore.Models;

namespace RetailStore.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RetailStoreDb;Trusted_Connection=True;");
    }
}
