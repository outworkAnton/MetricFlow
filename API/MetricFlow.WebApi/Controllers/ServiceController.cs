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
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceFlowService _serviceFlowService;

        public ServiceController(IServiceFlowService serviceFlowService)
        {
            _serviceFlowService = serviceFlowService;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            try
            {
                var services = _serviceFlowService.GetAll();
                return Ok(JsonConvert.SerializeObject(services));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}