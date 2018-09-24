using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DataAccess.Contract.Repositories;

using Microsoft.EntityFrameworkCore;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;
using DABaseModels = DataAccess.Models;

namespace DataAccess.Repositories
{
    public abstract class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
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
            IEnumerable items = null;
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                await Context.Locations.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.Location>>(
                    await Context.Locations.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                await Context.Services.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.Service>>(
                    await Context.Services.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                await Context.Metrics.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.Metric>>(
                    await Context.Metrics.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                await Context.Formulas.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.Formula>>(
                    await Context.Formulas.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                await Context.Statistics.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.Statistic>>(
                    await Context.Statistics.ToListAsync().ConfigureAwait(false));
            }

            if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                await Context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
                items = Mapper.Map<IEnumerable<DAContractModels.DatabaseRevision>>(
                    await Context.DatabaseRevisions.ToListAsync().ConfigureAwait(false));
            }

            return items?.OfType<T>().ToArray();
        }

        public virtual async Task Update(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                Context.Locations.Update(Mapper.Map<DABaseModels.Location>(item));
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                Context.Services.Update(Mapper.Map<DABaseModels.Service>(item));
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                Context.Metrics.Update(Mapper.Map<DABaseModels.Metric>(item));
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                Context.Formulas.Update(Mapper.Map<DABaseModels.Formula>(item));
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                Context.Statistics.Update(Mapper.Map<DABaseModels.Statistic>(item));
            }
            else
            {
                return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);

            await OnDatabaseChange().ConfigureAwait(false);
        }

        public virtual async Task Delete(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                Context.Locations.Remove(Mapper.Map<DABaseModels.Location>(item));
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                Context.Services.Remove(Mapper.Map<DABaseModels.Service>(item));
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                Context.Metrics.Remove(Mapper.Map<DABaseModels.Metric>(item));
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                Context.Formulas.Remove(Mapper.Map<DABaseModels.Formula>(item));
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                Context.Statistics.Remove(Mapper.Map<DABaseModels.Statistic>(item));
            }
            else if (typeof(DAContractInterfaces.IDatabaseRevision).IsAssignableFrom(typeof(T)))
            {
                Context.DatabaseRevisions.Remove(Mapper.Map<DABaseModels.DatabaseRevision>(item));
            }
            else
            {
                return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);

            await OnDatabaseChange().ConfigureAwait(false);
        }

        public virtual async Task<T> Create(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                await Context.Locations.AddAsync(Mapper.Map<DABaseModels.Location>(item))
                    .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                await Context.Services.AddAsync(Mapper.Map<DABaseModels.Service>(item))
                    .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                await Context.Metrics.AddAsync(Mapper.Map<DABaseModels.Metric>(item))
                    .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                await Context.Formulas.AddAsync(Mapper.Map<DABaseModels.Formula>(item))
                    .ConfigureAwait(false);
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                await Context.Statistics.AddAsync(Mapper.Map<DABaseModels.Statistic>(item))
                    .ConfigureAwait(false);
            }
            else
            {
                return null;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);

            await OnDatabaseChange().ConfigureAwait(false);
            var items = await Get().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg == item);
        }

        private async Task OnDatabaseChange()
        {
            await Context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
            var lastRevision = Context.DatabaseRevisions
                .Local
                .OrderByDescending(revision => revision.Modified)
                .FirstOrDefault();
            if (lastRevision.Changed == 0)
            {
                lastRevision.Changed = 1;
                Context.DatabaseRevisions.Update(lastRevision);
            }
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}