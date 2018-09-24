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
    public class MetricService : IMetricService
    {
        private readonly IMapper _mapper;
        private readonly IMetricRepository _repository;
        private IEnumerable<BLContractInterfaces.IMetric> _metrics;

        public MetricService(IMetricRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadMetrics();
        }

        private void LoadMetrics() => _metrics = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(metric => _mapper.Map<BLContractInterfaces.IMetric>(metric))
            .ToList();

        public IEnumerable<BLContractInterfaces.IMetric> GetAll()
        {
            return _metrics;
        }
    }
}