using AutoMapper;
using BaseModels = BusinessLogic.Models;
using ContractModels = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract;
using BL = BusinessLogic.Contract;

namespace BusinessLogic.DI
{
    public class BusinessLogicAutoMapperProfile : Profile
    {
        public BusinessLogicAutoMapperProfile()
        {
            CreateMap<DA.Interfaces.IDatabaseRevision, BL.Interfaces.IDatabaseRevision>()
                .Include<DA.Models.DatabaseRevision, BL.Models.DatabaseRevision>()
                .ReverseMap();
            CreateMap<DA.Interfaces.IFormula, BL.Interfaces.IFormula>()
                .Include<DA.Models.Formula, BL.Models.Formula>()
                .ReverseMap();
            CreateMap<DA.Interfaces.ILocation, BL.Interfaces.ILocation>()
                .Include<DA.Models.Location, BL.Models.Location>()
                .ReverseMap();
            CreateMap<DA.Interfaces.IMetric, BL.Interfaces.IMetric>()
                .Include<DA.Models.Metric, BL.Models.Metric>()
                .ReverseMap();
            CreateMap<DA.Interfaces.IService, BL.Interfaces.IService>()
                .Include<DA.Models.Service, BL.Models.Service>()
                .ReverseMap();
            CreateMap<DA.Interfaces.IStatistic, BL.Interfaces.IStatistic>()
                .Include<DA.Models.Statistic, BL.Models.Statistic>()
                .ReverseMap();

            CreateMap<DA.Models.DatabaseRevision, ContractModels.DatabaseRevision>()
                .ReverseMap();
            CreateMap<DA.Models.Formula, ContractModels.Formula>()
                .ReverseMap();
            CreateMap<DA.Models.Location, ContractModels.Location>()
                .ReverseMap();
            CreateMap<DA.Models.Metric, ContractModels.Metric>()
                .ReverseMap();
            CreateMap<DA.Models.Service, ContractModels.Service>()
                .ReverseMap();
            CreateMap<DA.Models.Statistic, ContractModels.Statistic>()
                .ReverseMap();

            CreateMap<BaseModels.DatabaseRevision, ContractModels.DatabaseRevision>()
                .ReverseMap();
            CreateMap<BaseModels.Formula, ContractModels.Formula>()
                .ReverseMap();
            CreateMap<BaseModels.Location, ContractModels.Location>()
                .ReverseMap();
            CreateMap<BaseModels.Metric, ContractModels.Metric>()
                .ReverseMap();
            CreateMap<BaseModels.Service, ContractModels.Service>()
                .ReverseMap();
            CreateMap<BaseModels.Statistic, ContractModels.Statistic>()
                .ReverseMap();
        }
    }
}