using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class MetricRepository : DataAccessRepository<Metric>, IMetricRepository
    {
        private DataAccessContext _context;
        private IMapper _mapper;

        public MetricRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}