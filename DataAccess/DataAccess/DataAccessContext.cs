﻿using DataAccess.Models;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DbSet<Location> Locations
        {
            get;
            set;
        }
        public DbSet<Service> Services
        {
            get;
            set;
        }
        public DbSet<Metric> Metrics
        {
            get;
            set;
        }
        public DbSet<Formula> Formulas
        {
            get;
            set;
        }
        public DbSet<Statistic> Statistics
        {
            get;
            set;
        }
        public DbSet<Log> Logs
        {
            get;
            set;
        }
        public string ConnectionString
        {
            get;
        }

        public DataAccessContext(IConfiguration configuration) => ConnectionString = configuration.GetSection("ConnectionStrings:MetricFlowDatabase")
            .Value;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection(ConnectionString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(connection);
        }
    }
}