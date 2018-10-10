using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DataAccess.Contract.Repositories;

using Microsoft.EntityFrameworkCore;

using Remotion.Linq.Utilities;
using DAContractModels = DataAccess.Contract.Models;
using DABaseModels = DataAccess.Models;

namespace DataAccess.Repositories
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class, new()
    {
        protected readonly DataAccessContext Context;
        protected readonly IMapper Mapper;

        protected DataAccessRepository(DataAccessContext context, IMapper mapper)
        {
            Context = context;
            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();
            Mapper = mapper;
        }

        public virtual async Task<IReadOnlyCollection<T>> Get()
        {
            var item = new T();
            IEnumerable items = null;

            switch (item)
            {
                case DAContractModels.Location _:
                    await Context.Locations.LoadAsync().ConfigureAwait(false);
                    items = Mapper.Map<IEnumerable<DAContractModels.Location>>(
                        await Context.Locations.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Service _:
                    await Context.Services.LoadAsync().ConfigureAwait(false);
                    items = Mapper.Map<IEnumerable<DAContractModels.Service>>(
                        await Context.Services.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Metric _:
                    await Context.Metrics.LoadAsync().ConfigureAwait(false);
                    items = Mapper.Map<IEnumerable<DAContractModels.Metric>>(
                        await Context.Metrics.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Formula _:
                    await Context.Formulas.LoadAsync().ConfigureAwait(false);
                    items = Mapper.Map<IEnumerable<DAContractModels.Formula>>(
                        await Context.Formulas.ToListAsync().ConfigureAwait(false));
                    break;
                case DAContractModels.Statistic _:
                    await Context.Statistics.LoadAsync().ConfigureAwait(false);
                    items = Mapper.Map<IEnumerable<DAContractModels.Statistic>>(
                        await Context.Statistics.ToListAsync().ConfigureAwait(false));
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
                        Context.Locations.Update(Mapper.Map<DABaseModels.Location>(item));
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        Context.Services.Update(Mapper.Map<DABaseModels.Service>(item));
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        Context.Metrics.Update(Mapper.Map<DABaseModels.Metric>(item));
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        Context.Formulas.Update(Mapper.Map<DABaseModels.Formula>(item));
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        Context.Statistics.Update(Mapper.Map<DABaseModels.Statistic>(item));
                        break;
                    }
                default:
                    return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> Find(string id)
        {
            var foundedItem = new T();
            switch (foundedItem)
            {
                case DAContractModels.Location _:
                    {
                        foundedItem = Mapper.Map<DAContractModels.Location>(await Context.Locations.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        foundedItem = Mapper.Map<DAContractModels.Service>(await Context.Services.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        foundedItem = Mapper.Map<DAContractModels.Metric>(await Context.Metrics.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        foundedItem = Mapper.Map<DAContractModels.Formula>(await Context.Formulas.FindAsync(id).ConfigureAwait(false)) as T;
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        foundedItem = Mapper.Map<DAContractModels.Statistic>(await Context.Statistics.FindAsync(id).ConfigureAwait(false)) as T;
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
                        Context.Locations.Remove(Mapper.Map<DABaseModels.Location>(item));
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        Context.Services.Remove(Mapper.Map<DABaseModels.Service>(item));
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        Context.Metrics.Remove(Mapper.Map<DABaseModels.Metric>(item));
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        Context.Formulas.Remove(Mapper.Map<DABaseModels.Formula>(item));
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        Context.Statistics.Remove(Mapper.Map<DABaseModels.Statistic>(item));
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task Create(T item)
        {
            switch (item)
            {
                case DAContractModels.Location _:
                    {
                        await Context.Locations.AddAsync(Mapper.Map<DABaseModels.Location>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Service _:
                    {
                        await Context.Services.AddAsync(Mapper.Map<DABaseModels.Service>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Metric _:
                    {
                        await Context.Metrics.AddAsync(Mapper.Map<DABaseModels.Metric>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Formula _:
                    {
                        await Context.Formulas.AddAsync(Mapper.Map<DABaseModels.Formula>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                case DAContractModels.Statistic _:
                    {
                        await Context.Statistics.AddAsync(Mapper.Map<DABaseModels.Statistic>(item))
                        .ConfigureAwait(false);
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}