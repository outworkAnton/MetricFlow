using AutoMapper;
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

            CreateMap<BLBaseModels.Formula, DAContractModels.Formula>()
                .ReverseMap();
            CreateMap<BLBaseModels.Location, DAContractModels.Location>()
                .ReverseMap();
            CreateMap<BLBaseModels.Metric, DAContractModels.Metric>()
                .ReverseMap();
            CreateMap<BLBaseModels.Service, DAContractModels.Service>()
                .ReverseMap();
            CreateMap<BLBaseModels.Statistic, DAContractModels.Statistic>()
                .ReverseMap();

            CreateMap<BLContractModels.Location, DAContractModels.Location>()
                .ReverseMap();
        }
    }
}