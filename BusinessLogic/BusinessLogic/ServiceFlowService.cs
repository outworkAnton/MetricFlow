using AutoMapper;

using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace BusinessLogic
{
    public class ServiceFlowService : BusinessLogicService<BL.IService, DA.IService>, IServiceFlowService
    {
        public ServiceFlowService(IServiceRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}