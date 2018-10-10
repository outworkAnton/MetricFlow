using System;
using System.Threading.Tasks;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Interfaces;
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
                return NotFound($"{typeof(TBL).Name} with Id: {id} not found");
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Update(item).ConfigureAwait(false);
                return Ok($"{typeof(TBL).Name} have been successfully updated");
            }
            catch
            {
                return NotFound($"{typeof(TBL).Name} not found");
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Delete(item).ConfigureAwait(false);
                return Ok($"{typeof(TBL).Name} have been successfully removed");
            }
            catch
            {
                return NotFound($"{typeof(TBL).Name} not found");
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateItem([FromBody] TBL item)
        {
            try
            {
                await _businessLogicService.Create(item).ConfigureAwait(false);
                return Ok($"{typeof(TBL).Name} have been successfuly created");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}