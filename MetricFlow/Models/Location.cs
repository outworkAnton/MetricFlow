using System.Collections.Generic;
using MetricFlow.Interfaces;

namespace MetricFlow.Models
{
    public class Location : ILocation
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public IEnumerable<IService> Services { get; set; }

        public Location(int locationId, string locationName, IEnumerable<IService> services)
        {
            LocationId = locationId;
            LocationName = locationName;
            Services = services;
        }
    }
}