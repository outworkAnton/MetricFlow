using AutoMapper;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class MetricRepository : DataAccessRepository<IMetric>, IMetricRepository
    {
        public MetricRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}