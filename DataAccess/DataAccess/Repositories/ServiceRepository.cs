using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class ServiceRepository : DataAccessRepository<Service>, IServiceRepository
    {
        private DataAccessContext _context;
        private IMapper _mapper;

        public ServiceRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}