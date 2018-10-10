using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using BusinessLogic.Contract.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BusinessLogic.Contract;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class MetricController : GenericController<BL.Metric,DA.Metric>
    {
        private readonly BLI.IMetricService _metricService;

        public MetricController(BLI.IMetricService metricService) : base(metricService)
        {
            _metricService = metricService;
        }
    }
}