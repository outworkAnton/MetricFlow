using System;
using System.Threading.Tasks;

using BusinessLogic.Contract;
using BusinessLogic.Models;

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
                var services = _serviceFlowService.GetAllItems();
                return Ok(JsonConvert.SerializeObject(services));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(string id)
        {
            try
            {
                var service = await _serviceFlowService.GetItemById(id).ConfigureAwait(false);
                return service == null
                    ? throw new Exception()
                    : Ok(JsonConvert.SerializeObject(service));
            }
            catch
            {
                return NotFound($"Service with Id: {id} not found");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] Service service)
        {
            try
            {
                var serviceForUpdate = await _serviceFlowService.GetItemById(service.Id).ConfigureAwait(false);
                serviceForUpdate.Name = service.Name;
                serviceForUpdate.Active = service.Active;
                await _serviceFlowService.Update(serviceForUpdate).ConfigureAwait(false);
                return Ok($"Service {service.Name} have been successfully updated");
            }
            catch
            {
                return NotFound($"Service {service.Name} not found");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteService([FromBody] Service service)
        {
            try
            {
                var serviceForDelete = await _serviceFlowService.GetItemById(service.Id).ConfigureAwait(false);
                await _serviceFlowService.Delete(serviceForDelete).ConfigureAwait(false);
                return Ok($"Service {service.Name} have been successfully removed");
            }
            catch
            {
                return NotFound($"Service {service.Name} not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] Service service)
        {
            try
            {
                await _serviceFlowService.Create(service).ConfigureAwait(false);
                var createdService = await _serviceFlowService.GetItemById(service.Id).ConfigureAwait(false);
                return createdService == null
                    ? throw new Exception("No errors in creation process but service didn't created")
                    : Ok(JsonConvert.SerializeObject(createdService));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}