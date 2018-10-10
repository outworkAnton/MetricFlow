using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class MetricRepository : DataAccessRepository<Metric>, IMetricRepository
    {
        public MetricRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}