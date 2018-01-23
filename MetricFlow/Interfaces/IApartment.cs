using System.Collections.Generic;

namespace MetricFlow.Interfaces
{
    public interface IApartment
    {
        int ApartmentId { get; set; }
        string ApartmentName { get; set; }
        IEnumerable<IService> Services { get; set; }
    }
}