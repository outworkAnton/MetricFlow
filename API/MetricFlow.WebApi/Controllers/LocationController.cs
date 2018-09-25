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
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IBusinessLogicService<ILocation> _locationService;

        public LocationController(IBusinessLogicService<ILocation> locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult GetAllLocations()
        {
            try
            {
                var locations = _locationService.GetAllItems();
                return Ok(JsonConvert.SerializeObject(locations));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(string id)
        {
            try
            {
                var revision = await _locationService.GetItemById(id).ConfigureAwait(false);
                return revision == null ? throw new Exception() : Ok(JsonConvert.SerializeObject(revision));
            }
            catch
            {
                return NotFound($"Location with Id: {id} not found");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(string id)
        {
            try
            {
                var locationForDelete = await _locationService.GetItemById(id).ConfigureAwait(false);
                await _locationService.Delete(locationForDelete).ConfigureAwait(false);
                return Ok($"Location with id {id} have been successfully removed");
            }
            catch
            {
                return NotFound($"Location with Id: {id} not found");
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateLocation([FromBody] string name, bool active)
        {
            try
            {
                var locationForDelete = await _locationService.GetItemById(id).ConfigureAwait(false);
                await _locationService.Delete(locationForDelete).ConfigureAwait(false);
                return Ok($"Location with id {id} have been successfully removed");
            }
            catch
            {
                return NotFound($"Location with Id: {id} not found");
            }
        }
    }
}