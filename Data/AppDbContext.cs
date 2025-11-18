using Microsoft.EntityFrameworkCore;
using ReportesAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("clients");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderDetail>().ToTable("orderdetails");

            modelBuilder.Entity<Client>()
                .Property(c => c.ClientId).HasColumnName("clientid");
            modelBuilder.Entity<Client>()
                .Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Client>()
                .Property(c => c.Email).HasColumnName("email");

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId).HasColumnName("productid");
            modelBuilder.Entity<Product>()
                .Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Product>()
                .Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnName("price");

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<Order>()
                .Property(o => o.ClientId).HasColumnName("clientid");
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate).HasColumnName("orderdate");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.OrderDetailId).HasColumnName("orderdetailid");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.ProductId).HasColumnName("productid");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Quantity).HasColumnName("quantity");
        }
    }
}