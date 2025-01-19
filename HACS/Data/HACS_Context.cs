using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            // Start from the bin directory and move to the root directory
            var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            // Combine the project root path with the relative path to the database
            var databasePath = Path.Combine(baseDirectory, "MRCModel", "Data", "HACS_database.db");


            Console.WriteLine(databasePath);

            // Ensure the relative path is correct and pass it to UseSqlite
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
    }
}
