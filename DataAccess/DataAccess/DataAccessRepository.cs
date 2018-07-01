using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Contract;
using Microsoft.EntityFrameworkCore;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;
using DABaseModels = DataAccess.Models;

namespace DataAccess
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
    {
        private readonly DataAccessContext _context;
        private readonly IMapper _mapper;

        protected DataAccessRepository(DataAccessContext context, IMapper mapper)
        {
            _context = context;
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _mapper = mapper;
        }

        public virtual async Task<IReadOnlyCollection<T>> Get()
        {
            IEnumerable items = null;
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                await _context.Locations.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.Location>>(
                    await _context.Locations.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                await _context.Services.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.Service>>(
                    await _context.Services.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                await _context.Metrics.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.Metric>>(
                    await _context.Metrics.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                await _context.Formulas.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.Formula>>(
                    await _context.Formulas.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                await _context.Statistics.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.Statistic>>(
                    await _context.Statistics.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                await _context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
                items = _mapper.Map<IEnumerable<DAContractModels.DatabaseRevision>>(
                    await _context.DatabaseRevisions.ToListAsync().ConfigureAwait(false));
            }

            return items?.OfType<T>().ToArray();
        }

        public virtual async Task Update(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                _context.Locations.Update(_mapper.Map<DABaseModels.Location>(item));
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                _context.Services.Update(_mapper.Map<DABaseModels.Service>(item));
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                _context.Metrics.Update(_mapper.Map<DABaseModels.Metric>(item));
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                _context.Formulas.Update(_mapper.Map<DABaseModels.Formula>(item));
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                _context.Statistics.Update(_mapper.Map<DABaseModels.Statistic>(item));
            }
            else if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                _context.DatabaseRevisions.Update(_mapper.Map<DABaseModels.DatabaseRevision>(item));
            }
            else
            {
                return;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public virtual async Task Delete(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                _context.Locations.Remove(_mapper.Map<DABaseModels.Location>(item));
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                _context.Services.Remove(_mapper.Map<DABaseModels.Service>(item));
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                _context.Metrics.Remove(_mapper.Map<DABaseModels.Metric>(item));
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                _context.Formulas.Remove(_mapper.Map<DABaseModels.Formula>(item));
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                _context.Statistics.Remove(_mapper.Map<DABaseModels.Statistic>(item));
            }
            else if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                _context.DatabaseRevisions.Remove(_mapper.Map<DABaseModels.DatabaseRevision>(item));
            }
            else
            {
                return;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            OnDatabaseChange();
        }

        public virtual async Task<T> Create(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
                await _context.Locations.AddAsync(_mapper.Map<DABaseModels.Location>(item))
                              .ConfigureAwait(false);
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                await _context.Services.AddAsync(_mapper.Map<DABaseModels.Service>(item))
                              .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                await _context.Metrics.AddAsync(_mapper.Map<DABaseModels.Metric>(item))
                              .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                await _context.Formulas.AddAsync(_mapper.Map<DABaseModels.Formula>(item))
                              .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                await _context.Statistics.AddAsync(_mapper.Map<DABaseModels.Statistic>(item))
                              .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                await _context.DatabaseRevisions.AddAsync(_mapper.Map<DABaseModels.DatabaseRevision>(item))
                              .ConfigureAwait(false);
            }
            else
            {
                return null;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);

            OnDatabaseChange();
            var items = await Get().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg == item);
        }

        private static void OnDatabaseChange()
        {
            //TODO : implement logic for mark and store database changes
        }
    }
}