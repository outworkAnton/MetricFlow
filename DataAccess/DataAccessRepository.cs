using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataAccessRepository : IDataAccessRepository
    {
        private readonly DataAccessContext _context = new DataAccessContext();

        public async Task<IEnumerable<T>> Get<T>()
        {
            if (typeof(T) == typeof(ILocation))
            {
                await _context.Locations.LoadAsync().ConfigureAwait(false);
                return _context.Locations.Local.AsEnumerable() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(IService))
            {
                await _context.Services.LoadAsync().ConfigureAwait(false);
                return _context.Services.Local.AsEnumerable() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(IMetric))
            {
                await _context.Metrics.LoadAsync().ConfigureAwait(false);
                return _context.Metrics.Local.AsEnumerable() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(IFormula))
            {
                await _context.Formulas.LoadAsync().ConfigureAwait(false);
                return _context.Formulas.Local.AsEnumerable() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(IStatistic))
            {
                await _context.Statistics.LoadAsync().ConfigureAwait(false);
                return _context.Statistics.Local.AsEnumerable() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(IDatabaseRevision))
            {
                await _context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
                return _context.DatabaseRevisions.Local.AsEnumerable() as IEnumerable<T>;
            }

            return null;
        }

        public Task Update<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> Create<T>(T item) where T : class
        {
            if (typeof(T) == typeof(ILocation))
            {
                await _context.Locations.LoadAsync().ConfigureAwait(false);
                await _context.Locations.AddAsync(item as Location).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as Location).Id).ConfigureAwait(false) as T;
            }

            if (typeof(T) == typeof(IService))
            {
                await _context.Services.LoadAsync().ConfigureAwait(false);
                await _context.Services.AddAsync(item as Service).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as Service).Id).ConfigureAwait(false) as T;
            }

            if (typeof(T) == typeof(IMetric))
            {
                await _context.Metrics.LoadAsync().ConfigureAwait(false);
                await _context.Metrics.AddAsync(item as Metric).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as Metric).Id).ConfigureAwait(false) as T;
            }

            if (typeof(T) == typeof(IFormula))
            {
                await _context.Formulas.LoadAsync().ConfigureAwait(false);
                await _context.Formulas.AddAsync(item as Formula).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as Formula).Id).ConfigureAwait(false) as T;
            }

            if (typeof(T) == typeof(IStatistic))
            {
                await _context.Statistics.LoadAsync().ConfigureAwait(false);
                await _context.Statistics.AddAsync(item as Statistic).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as Statistic).Id).ConfigureAwait(false) as T;
            }

            if (typeof(T) == typeof(IDatabaseRevision))
            {
                await _context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
                await _context.DatabaseRevisions.AddAsync(item as DatabaseRevision).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return await _context.Locations.FindAsync((item as DatabaseRevision).Id).ConfigureAwait(false) as T;
            }

            return null;
        }
    }
}