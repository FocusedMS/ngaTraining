using Microsoft.EntityFrameworkCore;
using SecureApp.Models;

namespace SecureApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<UserAccount> Users => Set<UserAccount>();
		public DbSet<FinancialRecord> FinancialRecords => Set<FinancialRecord>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserAccount>(entity =>
			{
				entity.HasIndex(u => u.Email).IsUnique();
				entity.Property(u => u.Email).HasMaxLength(256).IsRequired();
				entity.Property(u => u.PasswordHash).IsRequired();
				entity.Property(u => u.PasswordSalt).IsRequired();
			});

			modelBuilder.Entity<FinancialRecord>(entity =>
			{
				entity.Property(f => f.OwnerUserId).IsRequired();
				entity.Property(f => f.EncryptedPayload).IsRequired();
				entity.Property(f => f.PayloadHmac).IsRequired();
				entity.HasOne<UserAccount>()
					.WithMany()
					.HasForeignKey(f => f.OwnerUserId)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}


