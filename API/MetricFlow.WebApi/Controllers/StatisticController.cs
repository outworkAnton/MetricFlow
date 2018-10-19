using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController : GenericController<BL.Statistic, DA.Statistic>
    {
        private BLI.IStatisticService _statisticService;

        public StatisticController(BLI.IStatisticService statisticService) : base(statisticService)
        {
            _statisticService = statisticService;
        }
    }
}