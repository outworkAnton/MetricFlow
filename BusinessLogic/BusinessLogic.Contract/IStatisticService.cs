using System.Collections.Generic;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IStatisticService
    {
        IEnumerable<IStatistic> GetAll();
    }
}