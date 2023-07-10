using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ProductDbContext : DbContext
{
    public DbSet<MyUser> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity relationships, constraints, and other configurations using Fluent API if needed
        // Example: modelBuilder.Entity<User>().HasMany(u => u.Products).WithOne(p => p.User).HasForeignKey(p => p.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
