using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

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

        public override async Task Create(IDatabaseRevision databaseRevision)
        {
            await Context.DatabaseRevisions.AddAsync(Mapper.Map<DABaseModels.DatabaseRevision>(databaseRevision))
                .ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            var items = await Get().ConfigureAwait(false);
        }

        public override async Task Update(IDatabaseRevision databaseRevision)
        {
            Context.DatabaseRevisions.Update(Mapper.Map<DABaseModels.DatabaseRevision>(databaseRevision));
            await Context.SaveChangesAsync().ConfigureAwait(false);
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