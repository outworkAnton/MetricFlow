using DataAccess.Contract.Models;

namespace DataAccess.Contract
{
    public interface IDatabaseRevisionRepository : IDataAccessRepository<DatabaseRevision>
    {
        bool Changed();
    }
}