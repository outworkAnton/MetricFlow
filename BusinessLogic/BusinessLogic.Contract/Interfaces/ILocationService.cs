using BL = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface ILocationService : IBusinessLogicService<BL.Location, DA.Location>
        {

        }
}