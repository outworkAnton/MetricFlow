using System.Collections.Generic;
using BusinessLogic.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IFormulaService
    {
        IEnumerable<Formula> GetAll();
    }
}