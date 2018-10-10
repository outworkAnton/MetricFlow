using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;
using BusinessLogic.Contract.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BusinessLogic.Contract;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class MetricController : GenericController<BL.IMetric,DA.IMetric>
    {
        private readonly IMetricService _metricService;

        public MetricController(IMetricService metricService) : base(metricService)
        {
            _metricService = metricService;
        }
    }
}