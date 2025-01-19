using Microsoft.EntityFrameworkCore;
using MRCModel.Models;

namespace MRCModel.Data
{
    public class HACS_Context : DbContext
    {
        public static HACS_Context createContext()
        {
            return new HACS_Context();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AffectedIndividual> AffectedIndividuals { get; set; }
        public DbSet<AidOrganizationWorker> AidOrganizationWorkers { get; set; }
        public DbSet<GovernmentRepresentative> GovernmentRepresentatives { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HACS_database.db");
        }
    }
}
