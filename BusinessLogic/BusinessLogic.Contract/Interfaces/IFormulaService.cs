using BL = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IFormulaService : IBusinessLogicService<BL.Formula, DA.Formula>
    {
    }
}