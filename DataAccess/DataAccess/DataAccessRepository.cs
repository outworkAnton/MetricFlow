using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Contract;
using Microsoft.EntityFrameworkCore;
using ContractInterfaces = DataAccess.Contract.Interfaces;
using ContractModels = DataAccess.Contract.Models;
using BaseModels = DataAccess.Models;

namespace DataAccess
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
    {
        private readonly DataAccessContext _context;
        private readonly IMapper _mapper;

        protected DataAccessRepository(DataAccessContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable> Get()
        {
            if (typeof(ContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                await _context.Locations.LoadAsync().ConfigureAwait(false);
                return await _context.Locations.ToListAsync().ConfigureAwait(false);
            }

            if (typeof(ContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                await _context.Services.LoadAsync().ConfigureAwait(false);
                return await _context.Services.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
            }

            if (typeof(ContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                await _context.Metrics.LoadAsync().ConfigureAwait(false);
                return await _context.Metrics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
            }

            if (typeof(ContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                await _context.Formulas.LoadAsync().ConfigureAwait(false);
                return await _context.Formulas.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
            }

            if (typeof(ContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                await _context.Statistics.LoadAsync().ConfigureAwait(false);
                return await _context.Statistics.ToListAsync().ConfigureAwait(false) as IEnumerable<T>;
            }

            if (typeof(ContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                await _context.Revisions.LoadAsync().ConfigureAwait(false);
                return await _context.Revisions.ToListAsync().ConfigureAwait(false);
            }

            return null;
        }

        public virtual async Task Update(T item)
        {
//            switch (typeof(T))
//            {
//                case ILocation _:
//                    _context.Locations.Update(item as Location);
//                    break;
//                case IService _:
//                    _context.Services.Update(item as Service);
//                    break;
//                case IMetric _:
//                    _context.Metrics.Update(item as Metric);
//                    break;
//                case IFormula _:
//                    _context.Formulas.Update(item as Formula);
//                    break;
//                case IStatistic _:
//                    _context.Statistics.Update(item as Statistic);
//                    break;
//                case IDatabaseRevision _:
//                    _context.Revisions.Update(item as DatabaseRevision);
//                    break;
//                default:
//                    return;
//            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public virtual async Task Delete(T item)
        {
//            switch (typeof(T))
//            {
//                case ILocation _:
//                    _context.Locations.Remove(item as Location);
//                    break;
//                case IService _:
//                    _context.Services.Remove(item as Service);
//                    break;
//                case IMetric _:
//                    _context.Metrics.Remove(item as Metric);
//                    break;
//                case IFormula _:
//                    _context.Formulas.Remove(item as Formula);
//                    break;
//                case IStatistic _:
//                    _context.Statistics.Remove(item as Statistic);
//                    break;
//                case IDatabaseRevision _:
//                    _context.Revisions.Remove(item as DatabaseRevision);
//                    break;
//                default:
//                    return;
//            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public virtual async Task<T> Create(T item)
        {
            switch (item)
            {
                case ContractModels.Location _:
                    await _context.Locations.AddAsync(item as BaseModels.Location).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case ContractModels.Service _:
                    await _context.Services.AddAsync(item as BaseModels.Service).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case ContractModels.Metric _:
                    await _context.Metrics.AddAsync(item as BaseModels.Metric).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case ContractModels.Formula _:
                    await _context.Formulas.AddAsync(item as BaseModels.Formula).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case ContractModels.Statistic _:
                    await _context.Statistics.AddAsync(item as BaseModels.Statistic).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    break;
                case ContractModels.DatabaseRevision _:
                    await _context.Revisions.AddAsync(_mapper.Map<BaseModels.DatabaseRevision>(item)).ConfigureAwait(false);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
//                    createdItem = await _context.DatabaseRevisions.FindAsync((item as DatabaseRevision).Id)
//                                                .ConfigureAwait(false) as T;
                    break;
                default:
                    return null;
            }

            OnDatabaseChange();
            var items = await Get().ConfigureAwait(false);
            return items.OfType<T>().FirstOrDefault<T>(arg => arg == item);
        }

        private static void OnDatabaseChange()
        { }
    }
}