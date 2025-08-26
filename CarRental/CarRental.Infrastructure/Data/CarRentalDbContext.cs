using Microsoft.EntityFrameworkCore;
using CarRental.Core.Models;

namespace CarRental.Infrastructure.Data
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vehicle configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Make).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
                entity.Property(e => e.VehicleType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.DailyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(500);
                
                // Index for vehicle type and availability
                entity.HasIndex(e => new { e.VehicleType, e.IsAvailable });
            });

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(20);
                entity.Property(e => e.Country).HasMaxLength(50);
                entity.Property(e => e.DriverLicenseNumber).HasMaxLength(20);
                
                // Index for email search
                entity.HasIndex(e => e.Email);
            });

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.GitHubId).HasMaxLength(255);
                entity.Property(e => e.GitHubUsername).HasMaxLength(255);
                entity.Property(e => e.GitHubEmail).HasMaxLength(255);
                entity.Property(e => e.RefreshToken).HasMaxLength(255);
                
                // Indexes for authentication
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.GitHubId);
            });
        }
    }
}
