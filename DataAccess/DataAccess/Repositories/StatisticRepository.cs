using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class StatisticRepository : DataAccessRepository<Statistic>, IStatisticRepository
    {
        private DataAccessContext _context;
        private IMapper _mapper;

        public StatisticRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}