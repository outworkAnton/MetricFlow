using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DataAccess.Contract.Repositories;

using Microsoft.EntityFrameworkCore;
using DAContractModels = DataAccess.Contract.Models;
using DABaseModels = DataAccess.Models;

namespace DataAccess.Repositories
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class, new()
    {
        private readonly DataAccessContext _context;
        private readonly IMapper _mapper;

        protected DataAccessRepository(DataAccessContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<IReadOnlyCollection<T>> Get()
        {
            var item = new T();
            IEnumerable items = null;

            switch (item)
            {
                case DAContractModels.Location _:
                    await _context.Locations.LoadAsync().ConfigureAwait(false);
                    items = _mapper.Map<IEnumerable<DAContractModels.Location>>(
                        await _context.Locations.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Service _:
                    await _context.Services.LoadAsync().ConfigureAwait(false);
                    items = _mapper.Map<IEnumerable<DAContractModels.Service>>(
                        await _context.Services.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Metric _:
                    await _context.Metrics.LoadAsync().ConfigureAwait(false);
                    items = _mapper.Map<IEnumerable<DAContractModels.Metric>>(
                        await _context.Metrics.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Formula _:
                    await _context.Formulas.LoadAsync().ConfigureAwait(false);
                    items = _mapper.Map<IEnumerable<DAContractModels.Formula>>(
                        await _context.Formulas.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Statistic _:
                    await _context.Statistics.LoadAsync().ConfigureAwait(false);
                    items = _mapper.Map<IEnumerable<DAContractModels.Statistic>>(
                        await _context.Statistics.ToListAsync().ConfigureAwait(false));
                    break;
            }

            return items?.OfType<T>().ToArray();
        }

        public virtual async Task Update(T item)
        {
            switch (item)
            {
                case DAContractModels.Location _:
                    {
                        _context.Locations.Update(_mapper.Map<DABaseModels.Location>(item));
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        _context.Services.Update(_mapper.Map<DABaseModels.Service>(item));
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        _context.Metrics.Update(_mapper.Map<DABaseModels.Metric>(item));
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        _context.Formulas.Update(_mapper.Map<DABaseModels.Formula>(item));
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        _context.Statistics.Update(_mapper.Map<DABaseModels.Statistic>(item));
                        break;
                    }
                default:
                    return;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> Find(string id)
        {
            var foundedItem = new T();
            switch (foundedItem)
            {
                case DAContractModels.Location _:
                    {
                        foundedItem = _mapper.Map<DAContractModels.Location>(await _context.Locations.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        foundedItem = _mapper.Map<DAContractModels.Service>(await _context.Services.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        foundedItem = _mapper.Map<DAContractModels.Metric>(await _context.Metrics.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        foundedItem = _mapper.Map<DAContractModels.Formula>(await _context.Formulas.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        foundedItem = _mapper.Map<DAContractModels.Statistic>(await _context.Statistics.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            return foundedItem;
        }

        public virtual async Task Delete(T item)
        {
            switch (item)
            {
                case DAContractModels.Location _:
                    {
                        _context.Locations.Remove(_mapper.Map<DABaseModels.Location>(item));
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        _context.Services.Remove(_mapper.Map<DABaseModels.Service>(item));
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        _context.Metrics.Remove(_mapper.Map<DABaseModels.Metric>(item));
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        _context.Formulas.Remove(_mapper.Map<DABaseModels.Formula>(item));
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        _context.Statistics.Remove(_mapper.Map<DABaseModels.Statistic>(item));
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task Create(T item)
        {
            switch (item)
            {
                case DAContractModels.Location _:
                    {
                        await _context.Locations.AddAsync(_mapper.Map<DABaseModels.Location>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        await _context.Services.AddAsync(_mapper.Map<DABaseModels.Service>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        await _context.Metrics.AddAsync(_mapper.Map<DABaseModels.Metric>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        await _context.Formulas.AddAsync(_mapper.Map<DABaseModels.Formula>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        await _context.Statistics.AddAsync(_mapper.Map<DABaseModels.Statistic>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}