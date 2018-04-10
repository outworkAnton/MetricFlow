using System.Collections.Generic;
using System.Threading.Tasks;
using MetricFlow.Interfaces;
using static MetricFlow.Helpers.DataAccessProvider;
using MetricFlow.BLL;

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

        public Location(int locationId, string locationName)
        {
            LocationId = locationId;
            LocationName = locationName;
            Services = new List<IService>();
        }

        public Location() { }

        public async Task GetLocationServices()
        {
            Services = ConvertFromDAL<Service>(await new ServiceBLL().GetServicesByLocationId(LocationId).ConfigureAwait(false));
        }
    }
}