using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class ServiceRepository : DataAccessRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}