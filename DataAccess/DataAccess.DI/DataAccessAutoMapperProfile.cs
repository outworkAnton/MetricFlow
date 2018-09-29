using AutoMapper;
using BaseModels = DataAccess.Models;
using ContractModels = DataAccess.Contract.Models;

namespace DataAccess.DI
{
    public class DataAccessAutoMapperProfile : Profile
    {
        public DataAccessAutoMapperProfile()
        {
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