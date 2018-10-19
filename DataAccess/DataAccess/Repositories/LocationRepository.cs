using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class LocationRepository : DataAccessRepository<Location>, ILocationRepository
    {
        private DataAccessContext _context;
        private IMapper _mapper;
        
        public LocationRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}