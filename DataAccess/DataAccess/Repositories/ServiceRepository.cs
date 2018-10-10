using AutoMapper;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class ServiceRepository : DataAccessRepository<IService>, IServiceRepository
    {
        public ServiceRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}