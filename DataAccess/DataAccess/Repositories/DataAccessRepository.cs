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

            return items?.OfType<T>().ToArray();
        }

        public virtual async Task Update(T item)
        {
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                var oldItem = await Context.Locations.FindAsync((item as DABaseModels.Location).Id).ConfigureAwait(false);
                (item as DABaseModels.Location).Name = oldItem.Name;
                (item as DABaseModels.Location).Active = oldItem.Active;
                Context.Locations.Update(Mapper.Map<DABaseModels.Location>(item));
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                var oldItem = await Context.Services.FindAsync((item as DABaseModels.Service).Id).ConfigureAwait(false);
                (item as DABaseModels.Service).Name = oldItem.Name;
                (item as DABaseModels.Service).Active = oldItem.Active;
                Context.Services.Update(Mapper.Map<DABaseModels.Service>(item));
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                var oldItem = await Context.Metrics.FindAsync((item as DABaseModels.Metric).Id).ConfigureAwait(false);
                (item as DABaseModels.Metric).Name = oldItem.Name;
                (item as DABaseModels.Metric).Active = oldItem.Active;
                Context.Metrics.Update(Mapper.Map<DABaseModels.Metric>(item));
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                var oldItem = await Context.Formulas.FindAsync((item as DABaseModels.Formula).Id).ConfigureAwait(false);
                (item as DABaseModels.Formula).Name = oldItem.Name;
                (item as DABaseModels.Formula).Active = oldItem.Active;
                Context.Formulas.Update(Mapper.Map<DABaseModels.Formula>(item));
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                var oldItem = await Context.Statistics.FindAsync((item as DABaseModels.Statistic).Id).ConfigureAwait(false);
                (item as DABaseModels.Statistic).LocationId = oldItem.LocationId;
                (item as DABaseModels.Statistic).ServiceId = oldItem.ServiceId;
                (item as DABaseModels.Statistic).MetricId = oldItem.ServiceId;
                (item as DABaseModels.Statistic).FormulaId = oldItem.ServiceId;
                (item as DABaseModels.Statistic).Year = oldItem.Year;
                (item as DABaseModels.Statistic).Month = oldItem.Month;
                (item as DABaseModels.Statistic).Value = oldItem.Value;
                Context.Statistics.Update(Mapper.Map<DABaseModels.Statistic>(item));
            }
            else
            {
                return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> Find(string id)
        {
            T foundedItem;
            if (typeof(DAContractInterfaces.ILocation).IsAssignableFrom(typeof(T)))
            {
                foundedItem = await Context.Locations.FindAsync(id).ConfigureAwait(false)as T;
            }
            else if (typeof(DAContractInterfaces.IService).IsAssignableFrom(typeof(T)))
            {
                foundedItem = await Context.Services.FindAsync(id).ConfigureAwait(false)as T;
            }
            else if (typeof(DAContractInterfaces.IMetric).IsAssignableFrom(typeof(T)))
            {
                foundedItem = await Context.Metrics.FindAsync(id).ConfigureAwait(false)as T;
            }
            else if (typeof(DAContractInterfaces.IFormula).IsAssignableFrom(typeof(T)))
            {
                foundedItem = await Context.Formulas.FindAsync(id).ConfigureAwait(false)as T;
            }
            else if (typeof(DAContractInterfaces.IStatistic).IsAssignableFrom(typeof(T)))
            {
                foundedItem = await Context.Statistics.FindAsync(id).ConfigureAwait(false)as T;
            }
            else
            {
                return null;
            }

            return foundedItem;
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
            else
            {
                return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task Create(T item)
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
                return;
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}