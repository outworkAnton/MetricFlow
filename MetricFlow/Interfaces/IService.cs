using System.Collections.Generic;

namespace MetricFlow.Interfaces
{
    public interface IService
    {
        int ServiceId { get; set; }
        string ServiceName { get; set; }
        IEnumerable<IMetric> Metrics { get; set; }
    }
}