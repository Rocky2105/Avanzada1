using Microsoft.EntityFrameworkCore;
using NorthwindSqliteEntites;
using System.IO.Compression;
using System.Reflection.Metadata;
using static System.Console;
namespace NorthwindDataContext;

public class Northwind : DbContext
{
    public DbSet<Category>? Categories { get; set; }

    public DbSet<Customer>? Customers { get; set; }

    public DbSet<Employee>? Employees { get; set; }

    public DbSet<EmployeeTerritory>? EmployeeTerritories { get; set; }

    public DbSet<Order>? Orders { get; set; }

    public DbSet<OrderDetail>? OrderDetails { get; set; }

    public DbSet<Product>? Products { get; set; }

    public DbSet<Shipper>? Shippers { get; set; }

    public DbSet<Supplier>? Suppliers { get; set; }

    public DbSet<Territory>? Territories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Northwind.db");
        string connection = $"Filename={path}";
        ConsoleColor backgroundColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"Connection: {connection}");
        ForegroundColor = backgroundColor;
        optionsBuilder.UseSqlite(connection);
        //optionsBuilder.LogTo(WriteLine).EnableSensitiveDataLogging();

        // Using Lazy Loading
        optionsBuilder.UseLazyLoadingProxies();

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // FLUENT API
        // always use the model builder
        // Always define FIRST the ENTITY to Apply the Validations
        modelBuilder.Entity<Category>()
        .Property(category => category.CategoryName)
        .IsRequired()
        .HasMaxLength(15); // Then the property to validate

        // Fluent APi for cost on Product
        if(Database.ProviderName?.Contains("Sqlite") ?? false)
        {
            modelBuilder.Entity<Product>()
            .Property(product => product.Cost)
            .HasConversion<double>();

            modelBuilder.Entity<Category>()
            .Property(cat => cat.Description)
            .HasConversion<string>();

            modelBuilder.Entity<Category>()
            .Property(cat => cat.Picture)
            .HasConversion<byte[]>();

            modelBuilder.Entity<Employee>()
            .Property(cat => cat.Photo)
            .HasConversion<byte[]>();

            modelBuilder.Entity<Employee>()
            .Property(cat => cat.Notes)
            .HasConversion<string>();

            modelBuilder.Entity<Order>()
            .Property(cat => cat.Freight)
            .HasConversion<decimal>();


            modelBuilder.Entity<OrderDetail>()
            .Property(cat => cat.Cost)
            .HasConversion<decimal>();

            modelBuilder.Entity<OrderDetail>()
            .Property(cat => cat.Quantity)
            .HasConversion<short>();

            modelBuilder.Entity<OrderDetail>()
            .Property(cat => cat.Discount)
            .HasConversion<double>();

            modelBuilder.Entity<OrderDetail>()
            .Property(cat => cat.Cost)
            .HasConversion<decimal>();

            modelBuilder.Entity<Supplier>()
            .Property(cat => cat.HomePage)
            .HasConversion<string>();

            modelBuilder.Entity<Territory>()
            .Property(cat => cat.TerritoryDescription)
            .HasConversion<string>();
        }

        
        


        // Global filter to remove discontinued products
        modelBuilder.Entity<Product>()
        .HasQueryFilter(p => !p.Discontinued);

        // Eager Loading : Load data early
        // Lazy Loading : Load data automatically just before its needed
        // Explicit Loading : Load data manually

    }

}