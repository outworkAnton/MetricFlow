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
    public class StatisticService : IStatisticService
    {
        private readonly IMapper _mapper;
        private readonly IStatisticRepository _repository;
        private IEnumerable<BLContractInterfaces.IStatistic> _statistics;

        public StatisticService(IStatisticRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadStatistics();
        }

        private void LoadStatistics() => _statistics = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(statistic => _mapper.Map<BLContractInterfaces.IStatistic>(statistic))
            .ToList();

        public IEnumerable<BLContractInterfaces.IStatistic> GetAll()
        {
            return _statistics;
        }
    }
}