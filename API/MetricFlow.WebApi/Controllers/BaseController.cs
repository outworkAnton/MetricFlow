using System;
using System.Threading.Tasks;
using BusinessLogic.Contract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricFlow.WebApi.Controllers
{
    public abstract class GenericController<TBL,TDA> : ControllerBase where TBL: class where TDA: class
    {
        IBusinessLogicService<TBL,TDA> _businessLogicService;
        public GenericController(IBusinessLogicService<TBL,TDA> service)
        {
            _businessLogicService = service;
        }

        [HttpGet]
        public virtual IActionResult GetAllItems()
        {
            try
            {
                var services = _businessLogicService.GetAllItems();
                return Ok(JsonConvert.SerializeObject(services));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetItemById(string id)
        {
            try
            {
                var service = await _businessLogicService.GetItemById(id).ConfigureAwait(false);
                return service == null
                    ? throw new Exception()
                    : Ok(JsonConvert.SerializeObject(service));
            }
            catch
            {
                return NotFound($"{nameof(TBL)} with Id: {id} not found");
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Update(item).ConfigureAwait(false);
                return Ok($"{nameof(TBL)} have been successfully updated");
            }
            catch
            {
                return NotFound($"{nameof(TBL)} not found");
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Delete(item).ConfigureAwait(false);
                return Ok($"{nameof(TBL)} have been successfully removed");
            }
            catch
            {
                return NotFound($"{nameof(TBL)} not found");
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Create(item).ConfigureAwait(false);
                return Ok($"{nameof(TBL)} have been successfuly created");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}