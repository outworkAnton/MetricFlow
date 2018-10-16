﻿using AutoMapper;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class StatisticService : BusinessLogicService<BL.Statistic, DA.Statistic>, BLI.IStatisticService
    {
        public StatisticService(IStatisticRepository repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}