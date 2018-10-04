using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BusinessLogic;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;
using BusinessLogic.Contract.Interfaces;
using BusinessLogic.Contract.Models;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService) => _locationService = locationService;

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
                var location = await _locationService.GetItemById(id).ConfigureAwait(false);
                return location == null
                    ?
                    throw new Exception() : Ok(JsonConvert.SerializeObject(location));
            }
            catch
            {
                return NotFound($"Location with Id: {id} not found");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation([FromBody] Location location)
        {
            try
            {
                var locationForUpdate = await _locationService.GetItemById(location.Id).ConfigureAwait(false);
                locationForUpdate.Name = location.Name;
                locationForUpdate.Active = location.Active;
                await _locationService.Update(locationForUpdate).ConfigureAwait(false);
                return Ok($"Location {location.Name} have been successfully updated");
            }
            catch
            {
                return NotFound($"Location {location.Name} not found");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLocation([FromBody] Location location)
        {
            try
            {
                var locationForDelete = await _locationService.GetItemById(location.Id).ConfigureAwait(false);
                await _locationService.Delete(locationForDelete).ConfigureAwait(false);
                return Ok($"Location {location.Name} have been successfully removed");
            }
            catch
            {
                return NotFound($"Location {location.Name} not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] Location location)
        {
            try
            {
                await _locationService.Create(location).ConfigureAwait(false);
                var createdLocation = await _locationService.GetItemById(location.Id).ConfigureAwait(false);
                return createdLocation == null
                    ?
                    throw new Exception("No errors in creation process but location didn't created") : Ok(JsonConvert.SerializeObject(createdLocation));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}