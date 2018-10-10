using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;
using BusinessLogic.Contract.Interfaces;
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
    }
}