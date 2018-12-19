using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Log> Logs { get; set; }
        public string ConnectionString { get; }

        public DataAccessContext(IConfiguration configuration) => ConnectionString =
            configuration.GetConnectionString("MetricFlowDatabase");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}