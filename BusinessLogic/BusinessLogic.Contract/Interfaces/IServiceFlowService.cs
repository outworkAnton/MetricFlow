using BL = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IServiceFlowService : IBusinessLogicService<BL.Service, DA.Service>
        {

        }
}