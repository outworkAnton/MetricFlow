using System.Collections.Generic;
using System.Threading.Tasks;
using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface ILocationService : IBusinessLogicService<BL.ILocation, DA.ILocation>
        {

        }
}