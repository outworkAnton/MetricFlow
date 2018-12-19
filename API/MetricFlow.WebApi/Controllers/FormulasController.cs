using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [ApiController]
    public class FormulasController : GenericController<BL.Formula, DA.Formula>
    {
        private BLI.IFormulaService _formulaService;

        public FormulasController(BLI.IFormulaService formulaService) :base(formulaService)
        {
            _formulaService = formulaService;
        }
    }
}