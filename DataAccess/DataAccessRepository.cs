using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Contract;
using DataAccess.DataAccess.Contract;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataAccessRepository : IDataAccessRepository
    {
        private readonly DataAccessContext _context = new DataAccessContext();

        public async Task<IEnumerable<T>> Get<T>()
        {
            switch (typeof(T))
            {
                case ILocation _:
                    await _context.Locations.LoadAsync().ConfigureAwait(false);
                    return await _context.Locations.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IService _:
                    await _context.Services.LoadAsync().ConfigureAwait(false);
                    return await _context.Services.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IMetric _:
                    await _context.Metrics.LoadAsync().ConfigureAwait(false);
                    return await _context.Metrics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IFormula _:
                    await _context.Formulas.LoadAsync().ConfigureAwait(false);
                    return await _context.Formulas.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IStatistic _:
                    await _context.Statistics.LoadAsync().ConfigureAwait(false);
                    return await _context.Statistics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IDatabaseRevision _:
                    await _context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
                    return await _context.DatabaseRevisions.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                default:
                    return null;
            }
        }

        public async Task Update<T>(T item)
        {
            switch (typeof(T))
            {
                case ILocation _:
                    _context.Locations.Update(item as Location);
                    break;
                case IService _:
                    _context.Services.Update(item as Service);
                    break;
                case IMetric _:
                    _context.Metrics.Update(item as Metric);
                    break;
                case IFormula _:
                    _context.Formulas.Update(item as Formula);
                    break;
                case IStatistic _:
                    _context.Statistics.Update(item as Statistic);
                    break;
                case IDatabaseRevision _:
                    _context.DatabaseRevisions.Update(item as DatabaseRevision);
                    break;
                default:
                    return;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public async Task Delete<T>(T item)
        {
            switch (typeof(T))
            {
                case ILocation _:
                    _context.Locations.Remove(item as Location);
                    break;
                case IService _:
                    _context.Services.Remove(item as Service);
                    break;
                case IMetric _:
                    _context.Metrics.Remove(item as Metric);
                    break;
                case IFormula _:
                    _context.Formulas.Remove(item as Formula);
                    break;
                case IStatistic _:
                    _context.Statistics.Remove(item as Statistic);
                    break;
                case IDatabaseRevision _:
                    _context.DatabaseRevisions.Remove(item as DatabaseRevision);
                    break;
                default:
                    return;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public async Task<T> Create<T>(T item) where T : class
        {
//            T createdItem;
            switch (typeof(T))
            {
                case ILocation _:
                    await _context.Locations.AddAsync(item as Location).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IService _:
                    await _context.Services.AddAsync(item as Service).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IMetric _:
                    await _context.Metrics.AddAsync(item as Metric).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IFormula _:
                    await _context.Formulas.AddAsync(item as Formula).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IStatistic _:
                    await _context.Statistics.AddAsync(item as Statistic).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IDatabaseRevision _:
                    await _context.DatabaseRevisions.AddAsync(item as DatabaseRevision).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
//                    createdItem = await _context.DatabaseRevisions.FindAsync((item as DatabaseRevision).Id)
//                                                .ConfigureAwait(false) as T;
                    break;
                default:
                    return null;
            }

            OnDatabaseChange();
            var items = await Get<T>().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg == item);
        }

        private void OnDatabaseChange()
        { }
    }
}