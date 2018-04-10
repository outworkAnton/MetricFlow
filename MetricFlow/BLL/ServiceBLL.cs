using System.Data;
using System.Threading.Tasks;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class ServiceBLL
    {
        private const string TABLE_NAME = "Services";

        public async Task<DataTable> GetServiceById(int serviceId)
        {
            return await SelectFromByValue(TABLE_NAME, "Id", serviceId).ConfigureAwait(false);
        }

        public async Task<DataTable> GetServicesByLocationId(int locationId)
        {
            var parms = new StoredProcedureParameters();
            parms.AddParameter("Id", locationId);
            return await ExecuteStoredProcedure("GetServicesByLocationId", parms).ConfigureAwait(false);
        }
    }
}