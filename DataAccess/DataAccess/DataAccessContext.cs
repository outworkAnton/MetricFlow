using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<DatabaseRevision> DatabaseRevisions { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection("Data Source = Metric.Flow.Database;");
            optionsBuilder.UseSqlite(connection);
        }
    }
}