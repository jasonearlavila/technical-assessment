using Microsoft.EntityFrameworkCore;
using ProductCustomerDataFetcher.Models;

public class AppDbContext : DbContext
{
    public DbSet<Customer_Model> Customers { get; set; }

    public DbSet<Product_Model> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Replace with your actual SQL Server connection string
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}