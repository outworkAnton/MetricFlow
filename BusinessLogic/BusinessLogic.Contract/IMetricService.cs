using System.Collections.Generic;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IMetricService
    {
        IEnumerable<IMetric> GetAll();
    }
}