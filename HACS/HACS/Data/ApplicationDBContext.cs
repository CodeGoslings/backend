using HACS.Models;
using HACS.Models.DonorManagement;
using Microsoft.EntityFrameworkCore;

namespace HACS.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonationAdmin> DonationAdmins { get; set; }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Assignment>()
                .Property(a => a.Status)
                .HasConversion<int>();
        }
    }
}