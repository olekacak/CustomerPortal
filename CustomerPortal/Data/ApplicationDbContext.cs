using Microsoft.EntityFrameworkCore;
using CustomerPortal.Data;

namespace BlazorCRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("customer"); // 👈 Map class to real table
        }
    }
}
