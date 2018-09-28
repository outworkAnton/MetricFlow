using AutoMapper;

using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace BusinessLogic
{
    public class LocationService : BusinessLogicService<BL.ILocation, DA.ILocation>, ILocationService
    {
        public LocationService(ILocationRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}