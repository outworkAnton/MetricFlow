using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DataAccess.Contract;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;

using Microsoft.EntityFrameworkCore;
using DABaseModels = DataAccess.Models;

namespace DataAccess.Repositories
{
    public class DatabaseRevisionRepository : DataAccessRepository<IDatabaseRevision>, IDatabaseRevisionRepository
    {
        public DatabaseRevisionRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {}

        public async Task<bool> Changed()
        {
            var lastRevision = await GetLatestLocalRevision().ConfigureAwait(false);
            return lastRevision?.Changed == 1;
        }

        public override async Task<IDatabaseRevision> Create(IDatabaseRevision databaseRevision)
        {
            await Context.DatabaseRevisions.AddAsync(Mapper.Map<DABaseModels.DatabaseRevision>(databaseRevision))
                .ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            var items = await Get().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg.Id == databaseRevision.Id);
        }

        public async Task<IDatabaseRevision> GetLatestLocalRevision()
        {
            await Context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
            return Context.DatabaseRevisions?
                .Local
                .OrderByDescending(revision => revision.Modified)
                .FirstOrDefault();
        }
    }
}