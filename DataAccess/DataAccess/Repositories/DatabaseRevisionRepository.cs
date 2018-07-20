﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Contract;
using DataAccess.Contract.Models;
using DABaseModels = DataAccess.Models;

namespace DataAccess.Repositories
{
    public class DatabaseRevisionRepository : DataAccessRepository<DatabaseRevision>, IDatabaseRevisionRepository
    {
        public DatabaseRevisionRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        { }

        public override async Task<DatabaseRevision> Create(DatabaseRevision databaseRevision)
        {
            await Context.DatabaseRevisions.AddAsync(Mapper.Map<DABaseModels.DatabaseRevision>(databaseRevision))
                         .ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            var items = await Get().ConfigureAwait(false);
            return items.FirstOrDefault(arg => arg.Id == databaseRevision.Id);
        }
    }
}