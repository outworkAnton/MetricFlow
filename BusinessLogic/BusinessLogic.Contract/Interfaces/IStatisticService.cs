using BL = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IStatisticService : IBusinessLogicService<BL.Statistic, DA.Statistic>
    {

    }
}