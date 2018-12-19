using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [ApiController]
    public class ServicesController : GenericController<BL.Service, DA.Service>
    {
        private BLI.IServiceFlowService _serviceFlowService;

        public ServicesController(BLI.IServiceFlowService serviceFlowService) : base(serviceFlowService)
        {
            _serviceFlowService = serviceFlowService;
        }
    }
}