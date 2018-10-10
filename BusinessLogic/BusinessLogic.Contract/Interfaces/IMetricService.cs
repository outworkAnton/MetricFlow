using BL = BusinessLogic.Contract.Models;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IMetricService : IBusinessLogicService<BL.Metric, DA.Metric>
    {

    }
}