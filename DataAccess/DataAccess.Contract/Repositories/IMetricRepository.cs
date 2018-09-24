using System.Threading.Tasks;

using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;

namespace DataAccess.Contract.Repositories
{
    public interface IMetricRepository : IDataAccessRepository<IMetric>
    {

    }
}