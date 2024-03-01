using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;
//TODO: Kolla så att alla kopplinar används
public class GroceryStoreDbContext :DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }

    public GroceryStoreDbContext(DbContextOptions options) : base(options)
    {
        
    }

}