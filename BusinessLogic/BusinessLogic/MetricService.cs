using AutoMapper;

using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;

namespace BusinessLogic
{
    public class MetricService : BusinessLogicService<BL.IMetric, DA.IMetric>, IMetricService
    {
        public MetricService(IMetricRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}