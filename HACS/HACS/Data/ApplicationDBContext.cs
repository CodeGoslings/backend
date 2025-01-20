using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRCModel.Models;

namespace HACS.Data;

public class ApplicationDBContext : IdentityDbContext<User>
{
    // public DbSet<User> Users { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Donor> Donors { get; set; }
    public DbSet<DonationAdmin> DonationAdmins { get; set; }
    public DbSet<AffectedIndividual> AffectedIndividuals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=hacs;User Id=SA;Password=SqlServer1;TrustServerCertificate=True;Encrypt=false;");
    }
    
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .Entity<Assignment>()
            .Property(a => a.Status)
            .HasConversion<int>();
    }
}