using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class MetricController : ControllerBase
    {
        private readonly IMetricService _metricService;

        public MetricController(IMetricService metricService)
        {
            _metricService = metricService;
        }

        [HttpGet]
        public IActionResult GetAllMetrics()
        {
            try
            {
                var metrics = _metricService.GetAll();
                return Ok(JsonConvert.SerializeObject(metrics));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}