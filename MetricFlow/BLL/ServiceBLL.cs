using System.Data;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class ServiceBLL
    {
        private const string TABLE_NAME = "Services";

        public DataTable GetServiceById(int serviceId)
        {
            return SelectFromByValue(TABLE_NAME, "Id", serviceId);
        }

        public DataTable GetServicesByLocationId(int locationId)
        {
            var parms = new StoredProcedureParameters();
            parms.AddParameter("Id", locationId);
            return ExecuteStoredProcedure("GetServicesByLocationId", parms);
        }
    }
}