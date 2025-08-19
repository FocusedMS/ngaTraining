using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CmsProject.Models
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext() { }
        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Vendor>().ToTable("Vendor");
            modelBuilder.Entity<Wallet>().ToTable("Wallet");

            // Map exact column names (matches your SQL)
            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(x => x.CustId);
                e.Property(x => x.CustId).HasColumnName("custId");
                e.Property(x => x.CustName).HasColumnName("custName");
                e.Property(x => x.CustUserName).HasColumnName("custUserName");
                e.Property(x => x.CustPassword).HasColumnName("custPassword");
                e.Property(x => x.City).HasColumnName("city");
                e.Property(x => x.State).HasColumnName("state");
                e.Property(x => x.Email).HasColumnName("email");
                e.Property(x => x.MobileNo).HasColumnName("mobileNo");
            });
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Wallet> Wallets { get; set; } = null!;
    }
}
