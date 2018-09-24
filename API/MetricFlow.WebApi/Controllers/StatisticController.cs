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
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public IActionResult GetAllStatistics()
        {
            try
            {
                var statistics = _statisticService.GetAll();
                return Ok(JsonConvert.SerializeObject(statistics));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}