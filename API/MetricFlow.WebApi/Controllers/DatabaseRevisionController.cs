using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLogic.Contract;
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
            var changed = _revisionService.Changed();
            return Ok(JsonConvert.SerializeObject(changed));
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadLatestRevision()
        {
            await _revisionService.DownloadLatestDatabaseRevision().ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetRevisionById(string id)
        {
            return new ObjectResult(_revisionService.GetRevisionById(id));
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