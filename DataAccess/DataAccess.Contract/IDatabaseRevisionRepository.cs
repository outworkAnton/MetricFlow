using System.Threading.Tasks;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;

namespace DataAccess.Contract
{
    public interface IDatabaseRevisionRepository : IDataAccessRepository<IDatabaseRevision>
        {
            Task<bool> Changed();
            Task<IDatabaseRevision> GetLatestLocalRevision();
        }
}