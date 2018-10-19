using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/formulas")]
    [ApiController]
    public class FormulaController : GenericController<BL.Formula, DA.Formula>
    {
        private BLI.IFormulaService _formulaService;

        public FormulaController(BLI.IFormulaService formulaService) :base(formulaService)
        {
            _formulaService = formulaService;
        }
    }
}