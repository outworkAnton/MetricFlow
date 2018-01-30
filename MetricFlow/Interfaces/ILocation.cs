using System.Collections.Generic;

namespace MetricFlow.Interfaces
{
    public interface ILocation
    {
        int LocationId { get; set; }
        string LocationName { get; set; }
        IEnumerable<IService> Services { get; set; }
    }
}