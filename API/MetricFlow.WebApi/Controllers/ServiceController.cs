using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceController : GenericController<BL.Service, DA.Service>
    {
        public ServiceController(BLI.IServiceFlowService serviceFlowService) : base(serviceFlowService) { }
    }
}