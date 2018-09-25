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
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _repository;
        private IEnumerable<BLContractInterfaces.ILocation> _locations;

        public LocationService(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadLocations();
        }

        private void LoadLocations() => _locations = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(location => _mapper.Map<BLContractInterfaces.ILocation>(location))
            .ToList();

        public IEnumerable<BLContractInterfaces.ILocation> GetAll()
        {
            return _locations;
        }

        public BLContractInterfaces.ILocation GetLocationById(string id)
        {
            return _locations?.FirstOrDefault(location => location.Id == id);
        }
    }
}