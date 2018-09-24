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
    [Route("api/formulas")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private readonly IFormulaService _formulaService;

        public FormulaController(IFormulaService formulaService)
        {
            _formulaService = formulaService;
        }

        [HttpGet]
        public IActionResult GetAllFormulas()
        {
            try
            {
                var formulas = _formulaService.GetAll();
                return Ok(JsonConvert.SerializeObject(formulas));
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}