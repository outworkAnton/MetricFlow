﻿using AutoMapper;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class MetricService : BusinessLogicService<BL.Metric, DA.Metric>, BLI.IMetricService
    {
        private IMetricRepository _repository;
        private IMapper _mapper;

        public MetricService(IMetricRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}