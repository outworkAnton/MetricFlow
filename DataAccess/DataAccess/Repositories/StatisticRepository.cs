using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class StatisticRepository : DataAccessRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}