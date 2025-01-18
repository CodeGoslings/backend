using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HACS.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<DonationAdmin> DonationAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=192.168.68.114;Database=hacs;User Id=SA;Password=SqlServer1;TrustServerCertificate=True;Encrypt=false;");
    }
}