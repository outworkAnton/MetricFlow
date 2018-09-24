using System.Collections.Generic;

using AutoMapper;

using BusinessLogic.Contract;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using BLContractModels = BusinessLogic.Contract.Models;
using DataAccess.Contract.Repositories;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;
using System;
using System.Linq;

namespace BusinessLogic
{
    public class ServiceFlowService : IServiceFlowService
    {
        private readonly IMapper _mapper;
        private readonly IServiceRepository _repository;
        private IEnumerable<BLContractInterfaces.IService> _services;

        public ServiceFlowService(IServiceRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadServices();
        }

        private void LoadServices() => _services = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(service => _mapper.Map<BLContractInterfaces.IService>(service))
            .ToList();

        public IEnumerable<BLContractInterfaces.IService> GetAll()
        {
            return _services;
        }
    }
}