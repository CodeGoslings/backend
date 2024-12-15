using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;
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
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<VolunteerContract> VolunteerContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<VolunteerContract>(x => x.HasKey(vc => new { vc.VolunteerId, vc.OrganizationId }));

            modelBuilder
                .Entity<VolunteerContract>()
                .HasOne(vc => vc.Volunteer)
                .WithMany(v => v.VolunteerContracts)
                .HasForeignKey(vc => vc.VolunteerId);

            modelBuilder
                .Entity<VolunteerContract>()
                .HasOne(vc => vc.Organization)
                .WithMany(o => o.VolunteerContracts)
                .HasForeignKey(vc => vc.OrganizationId);

            modelBuilder
                .Entity<Assignment>()
                .Property(a => a.Status)
                .HasConversion<int>();
        }
    }
}