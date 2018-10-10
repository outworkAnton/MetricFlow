using BL = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IMetricService : IBusinessLogicService<BL.IMetric, DA.IMetric>
    {

    }
}