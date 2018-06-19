﻿using AutoMapper;
using BLBaseModels = BusinessLogic.Models;
using BLContractModels = BusinessLogic.Contract.Models;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;
using DAContractInterfaces = DataAccess.Contract.Interfaces;

namespace BusinessLogic.DI
{
    public class BusinessLogicAutoMapperProfile : Profile
    {
        public BusinessLogicAutoMapperProfile()
        {
            CreateMap<DAContractInterfaces.IDatabaseRevision, BLContractInterfaces.IDatabaseRevision>()
                .Include<DAContractModels.DatabaseRevision, BLContractModels.DatabaseRevision>();
            CreateMap<DAContractInterfaces.IFormula, BLContractInterfaces.IFormula>()
                .Include<DAContractModels.Formula, BLContractModels.Formula>();
            CreateMap<DAContractInterfaces.ILocation, BLContractInterfaces.ILocation>()
                .Include<DAContractModels.Location, BLContractModels.Location>();
            CreateMap<DAContractInterfaces.IMetric, BLContractInterfaces.IMetric>()
                .Include<DAContractModels.Metric, BLContractModels.Metric>();
            CreateMap<DAContractInterfaces.IService, BLContractInterfaces.IService>()
                .Include<DAContractModels.Service, BLContractModels.Service>();
            CreateMap<DAContractInterfaces.IStatistic, BLContractInterfaces.IStatistic>()
                .Include<DAContractModels.Statistic, BLContractModels.Statistic>();

            CreateMap<DAContractModels.DatabaseRevision, BLContractModels.DatabaseRevision>()
                .ReverseMap();
            CreateMap<DAContractModels.Formula, BLContractModels.Formula>()
                .ReverseMap();
            CreateMap<DAContractModels.Location, BLContractModels.Location>()
                .ReverseMap();
            CreateMap<DAContractModels.Metric, BLContractModels.Metric>()
                .ReverseMap();
            CreateMap<DAContractModels.Service, BLContractModels.Service>()
                .ReverseMap();
            CreateMap<DAContractModels.Statistic, BLContractModels.Statistic>()
                .ReverseMap();

            CreateMap<BLBaseModels.DatabaseRevision, BLContractModels.DatabaseRevision>()
                .ReverseMap();
            CreateMap<BLBaseModels.Formula, BLContractModels.Formula>()
                .ReverseMap();
            CreateMap<BLBaseModels.Location, BLContractModels.Location>()
                .ReverseMap();
            CreateMap<BLBaseModels.Metric, BLContractModels.Metric>()
                .ReverseMap();
            CreateMap<BLBaseModels.Service, BLContractModels.Service>()
                .ReverseMap();
            CreateMap<BLBaseModels.Statistic, BLContractModels.Statistic>()
                .ReverseMap();

            CreateMap<BLBaseModels.DatabaseRevision, DAContractModels.DatabaseRevision>()
                .ReverseMap();
//            CreateMap<BLBaseModels.Formula, DAContractModels.Formula>();
//            CreateMap<BLBaseModels.Location, DAContractModels.Location>();
//            CreateMap<BLBaseModels.Metric, DAContractModels.Metric>();
//            CreateMap<BLBaseModels.Service, DAContractModels.Service>();
//            CreateMap<BLBaseModels.Statistic, DAContractModels.Statistic>();
        }
    }
}