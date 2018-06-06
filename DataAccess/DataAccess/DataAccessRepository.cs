using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Contract;
using DataAccess.Contract.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
    {
        protected readonly DataAccessContext Context;

        protected DataAccessRepository(DataAccessContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<T>> Get()
        {
            switch (typeof(T))
            {
                case ILocation _:
                    await Context.Locations.LoadAsync().ConfigureAwait(false);
                    return await Context.Locations.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IService _:
                    await Context.Services.LoadAsync().ConfigureAwait(false);
                    return await Context.Services.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IMetric _:
                    await Context.Metrics.LoadAsync().ConfigureAwait(false);
                    return await Context.Metrics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IFormula _:
                    await Context.Formulas.LoadAsync().ConfigureAwait(false);
                    return await Context.Formulas.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IStatistic _:
                    await Context.Statistics.LoadAsync().ConfigureAwait(false);
                    return await Context.Statistics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                case IDatabaseRevision _:
                    await Context.Revisions.LoadAsync().ConfigureAwait(false);
                    return await Context.Revisions.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
                default:
                    return null;
            }
        }

        public async Task Update(T item)
        {
            switch (typeof(T))
            {
                case ILocation _:
                    Context.Locations.Update(item as Location);
                    break;
                case IService _:
                    Context.Services.Update(item as Service);
                    break;
                case IMetric _:
                    Context.Metrics.Update(item as Metric);
                    break;
                case IFormula _:
                    Context.Formulas.Update(item as Formula);
                    break;
                case IStatistic _:
                    Context.Statistics.Update(item as Statistic);
                    break;
                case IDatabaseRevision _:
                    Context.Revisions.Update(item as Revision);
                    break;
                default:
                    return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public async Task Delete(T item)
        {
            switch (typeof(T))
            {
                case ILocation _:
                    Context.Locations.Remove(item as Location);
                    break;
                case IService _:
                    Context.Services.Remove(item as Service);
                    break;
                case IMetric _:
                    Context.Metrics.Remove(item as Metric);
                    break;
                case IFormula _:
                    Context.Formulas.Remove(item as Formula);
                    break;
                case IStatistic _:
                    Context.Statistics.Remove(item as Statistic);
                    break;
                case IDatabaseRevision _:
                    Context.Revisions.Remove(item as Revision);
                    break;
                default:
                    return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public async Task<T> Create(T item)
        {
//            T createdItem;
            switch (typeof(T))
            {
                case ILocation _:
                    await Context.Locations.AddAsync(item as Location).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IService _:
                    await Context.Services.AddAsync(item as Service).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IMetric _:
                    await Context.Metrics.AddAsync(item as Metric).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IFormula _:
                    await Context.Formulas.AddAsync(item as Formula).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IStatistic _:
                    await Context.Statistics.AddAsync(item as Statistic).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case IDatabaseRevision _:
                    await Context.Revisions.AddAsync(item as Revision).ConfigureAwait(false);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
//                    createdItem = await _context.DatabaseRevisions.FindAsync((item as DatabaseRevision).Id)
//                                                .ConfigureAwait(false) as T;
                    break;
                default:
                    return null;
            }

            OnDatabaseChange();
            var items = await Get().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg == item);
        }

        private void OnDatabaseChange()
        { }
    }
}