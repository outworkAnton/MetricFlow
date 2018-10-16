using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class LocationRepository : DataAccessRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataAccessContext context, IMapper mapper) : base(context, mapper) { }
    }
}