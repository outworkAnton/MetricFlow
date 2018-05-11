using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DatabaseRevisionRepository : DataAccessBaseRepository, IDatabaseRevisionRepository
    {
        public async Task<IEnumerable<DatabaseRevision>> Get()
        {
            await Context.DatabaseRevisions.LoadAsync().ConfigureAwait(false);
            return Context.DatabaseRevisions.Local.AsEnumerable();
        }

        public Task Update(DatabaseRevision item)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(DatabaseRevision item)
        {
            throw new System.NotImplementedException();
        }

        public Task<DatabaseRevision> Create()
        {
            throw new System.NotImplementedException();
        }
    }
}