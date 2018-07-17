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
    [Route("api/revisions")]
    [ApiController]
    public class DatabaseRevisionController : ControllerBase
    {
        private readonly IRevisionService _revisionService;

        public DatabaseRevisionController(IRevisionService revisionService)
        {
            _revisionService = revisionService;
        }

        [HttpGet]
        public IActionResult GetAllRevisions()
        {
            var revisions = _revisionService.GetAll();
            return Ok(JsonConvert.SerializeObject(revisions));
        }

        [HttpGet("changed")]
        public IActionResult IsDatabaseChanged()
        {
            bool changed = _revisionService.Changed();
            return Ok(JsonConvert.SerializeObject(changed));
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadLatestRevision()
        {
            try
            {
                await _revisionService.DownloadLatestDatabaseRevision().ConfigureAwait(false);
                return Ok();
            }
            catch (ServiceException serviceException)
            {
                return StatusCode(503, serviceException.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRevisionById(string id)
        {
            try
            {
                var revision = _revisionService.GetRevisionById(id) ?? throw new Exception();
                return Ok(revision);
            }
            catch (Exception e)
            {
                return NotFound($"Revision with Id: {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { }
    }
}