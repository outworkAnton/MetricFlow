using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace DataAccess.Interfaces
{
    public interface IDatabaseRevisionRepository
    {
        Task<IEnumerable<DatabaseRevision>> Get();
        Task Update(DatabaseRevision item);
        Task Delete(DatabaseRevision item);
        Task<DatabaseRevision> Create();
    }
}