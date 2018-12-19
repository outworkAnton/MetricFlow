using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [ApiController]
    public class StatisticsController : GenericController<BL.Statistic, DA.Statistic>
    {
        private BLI.IStatisticService _statisticService;

        public StatisticsController(BLI.IStatisticService statisticService) : base(statisticService)
        {
            _statisticService = statisticService;
        }
    }
}