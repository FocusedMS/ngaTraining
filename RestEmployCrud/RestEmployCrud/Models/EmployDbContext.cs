using Microsoft.EntityFrameworkCore;

namespace RestEmployCrud.Models
{
    public class EmployDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public EmployDbContext(DbContextOptions<EmployDbContext> options) : base(options)
        {
        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=anandanitha\\sqlexpress;Database=Wiprojuly;Trusted_Connection=True;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employ>().ToTable("Employee");
        }

        public DbSet<Employ> Employees { get; set; }
    }
}