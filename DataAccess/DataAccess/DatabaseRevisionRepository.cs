using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Contract;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DatabaseRevisionRepository : DataAccessRepository<DatabaseRevision>, IDatabaseRevisionRepository
    {
        public DatabaseRevisionRepository(DataAccessContext context) : base(context)
        { }

        public new async Task<IEnumerable<IDatabaseRevision>> Get()
        {
            await Context.Revisions.LoadAsync().ConfigureAwait(false);
            return await Context.Revisions.ToListAsync().ConfigureAwait(false);
        }

        public Task Update(IDatabaseRevision item)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(IDatabaseRevision item)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDatabaseRevision> Create(IDatabaseRevision item)
        {
            throw new System.NotImplementedException();
        }
    }
}