using System.Threading.Tasks;

using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Models;

namespace DataAccess.Contract.Repositories
{
    public interface IFormulaRepository : IDataAccessRepository<IFormula>
    {}
}