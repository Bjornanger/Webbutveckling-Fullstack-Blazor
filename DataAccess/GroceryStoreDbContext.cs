using Microsoft.EntityFrameworkCore;
using webbutveckling_labb2_Bjornanger.Shared.Entities;

namespace DataAccess;

public class GroceryStoreDbContext :DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }

    public DbSet<ProductsOrders> ProductOrders { get; set; }

    public GroceryStoreDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductsOrders>()
            .HasKey(po => new { po.OrderId, po.ProductId });

        modelBuilder.Entity<ProductsOrders>()
            .HasOne(sc => sc.Product)
            .WithMany(s => s.ProductOrders)
            .HasForeignKey(sc => sc.ProductId);

        modelBuilder.Entity<ProductsOrders>()
            .HasOne(sc => sc.Order)
            .WithMany(c => c.ProductOrders)
            .HasForeignKey(sc => sc.OrderId);
    }

}