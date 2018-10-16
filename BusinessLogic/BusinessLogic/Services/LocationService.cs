using AutoMapper;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class LocationService : BusinessLogicService<BL.Location, DA.Location>, BLI.ILocationService
    {
        public LocationService(ILocationRepository repository, IMapper mapper) : base(repository, mapper) { }
    }
}