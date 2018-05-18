using AutoMapper;
using BaseModels = BusinessLogic.Models;
using ContractModels = BusinessLogic.Contract.Models;

namespace BusinessLogic.DI
{
    public class BusinessLogicAutoMapperProfile : Profile
    {
        public BusinessLogicAutoMapperProfile()
        {
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