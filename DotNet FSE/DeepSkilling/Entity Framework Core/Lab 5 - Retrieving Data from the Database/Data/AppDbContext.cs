using Microsoft.EntityFrameworkCore;
using RetailQuery.Models;

namespace RetailQuery.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RetailQueryDb;Trusted_Connection=True;");
    }
}
