using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [ApiController]
    public class MetricsController : GenericController<BL.Metric,DA.Metric>
    {
        private BLI.IMetricService _metricService;

        public MetricsController(BLI.IMetricService metricService) : base(metricService)
        {
            _metricService = metricService;
        }
    }
}