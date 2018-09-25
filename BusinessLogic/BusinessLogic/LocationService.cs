using AutoMapper;

using BusinessLogic.Contract;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using BLContractModels = BusinessLogic.Contract.Models;
using DataAccess.Contract.Repositories;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;

namespace BusinessLogic
{
    public class LocationService : BusinessLogicService<BLContractInterfaces.ILocation>, ILocationService
    {
        public LocationService(IDataAccessRepository<BLContractInterfaces.ILocation> repository, IMapper mapper) : base(repository, mapper)
        {
            
        }
    }
}