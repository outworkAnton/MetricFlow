using AutoMapper;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class ServiceFlowService : BusinessLogicService<BL.Service, DA.Service>, BLI.IServiceFlowService
    {
        private IServiceRepository _repository;
        private IMapper _mapper;

        public ServiceFlowService(IServiceRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}