using AutoMapper;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class LocationRepository : DataAccessRepository<ILocation>, ILocationRepository
    {
        public LocationRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}