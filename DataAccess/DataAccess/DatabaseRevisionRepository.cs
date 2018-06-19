using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Contract;
using DataAccess.Contract.Models;

namespace DataAccess
{
    public class DatabaseRevisionRepository : DataAccessRepository<DatabaseRevision>, IDatabaseRevisionRepository
    {
        private readonly IMapper _mapper;

        public DatabaseRevisionRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override async Task<IEnumerable> Get()
        {
            return await base.Get().ConfigureAwait(false);
        }

        public override async Task Update(DatabaseRevision databaseRevision)
        {
            await base.Update(databaseRevision).ConfigureAwait(false);
        }

        public override async Task Delete(DatabaseRevision databaseRevision)
        {
            await base.Delete(databaseRevision).ConfigureAwait(false);
        }

        public override async Task<DatabaseRevision> Create(DatabaseRevision databaseRevision)
        {
            return await base.Create(databaseRevision).ConfigureAwait(false);
        }
    }
}