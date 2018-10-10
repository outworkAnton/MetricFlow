using AutoMapper;
using BusinessLogic.Contract;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class MetricService : BusinessLogicService<BL.Metric, DA.Metric>, BLI.IMetricService
    {
        public MetricService(IMetricRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}