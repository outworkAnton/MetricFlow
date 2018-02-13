using System.Collections.Generic;
using MetricFlow.Interfaces;

namespace MetricFlow.Models
{
    public class Service : IService
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public IEnumerable<IMetric> Metrics { get; set; }

        public Service(int serviceId, string serviceName, IEnumerable<IMetric> metrics)
        {
            ServiceId = serviceId;
            ServiceName = serviceName;
            Metrics = metrics;
        }

        public Service(int serviceId, string serviceName)
        {
            ServiceId = serviceId;
            ServiceName = serviceName;
            Metrics = new List<IMetric>();
        }

        public Service() { }
    }
}