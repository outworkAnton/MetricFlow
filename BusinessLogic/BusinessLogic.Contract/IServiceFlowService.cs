using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IServiceFlowService : IBusinessLogicService<BL.IService, DA.IService>
        {

        }
}