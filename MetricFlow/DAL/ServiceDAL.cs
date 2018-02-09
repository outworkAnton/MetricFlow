using System.Data;
using MetricFlow.Helpers;

namespace MetricFlow.DAL
{
    public class ServiceDAL
    {
        public DataTable GetServiceById(int serviceId)
        {
            var query = "SELECT * " +
                        "FROM Services" +
                        $"WHERE Id={serviceId}";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public DataTable GetServicesByLocationId(int locationId)
        {
            var query = "SELECT * " +
                        "FROM Locations l" +
                        "JOIN Services s ON l.Id = s.LocationId" +
                        $"WHERE l.Id={locationId}";
            return DBHelper.ExecuteSelectQuery(query);
        }
    }
}