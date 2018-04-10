using System.Data;
using System.Threading.Tasks;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class StatisticBLL
    {
        private const string TABLE_NAME = "Statistics";

        public async Task<DataTable> GetAllStatistics()
        {
            return await SelectAllFrom(TABLE_NAME).ConfigureAwait(false);
        }

        public async Task<DataTable> GetStatisticsByLocation(int locationId)
        {
            return await SelectFromByValue(TABLE_NAME, "LocationId", locationId).ConfigureAwait(false);
        }

        public async Task<DataTable> GetStatisticsByPeriod(int locationId, int year, int[] months)
        {
            var parms = new StoredProcedureParameters();
            parms.AddParameter("LocationId", locationId);
            parms.AddParameter("Year", year);
            parms.AddParameter("Month", months);
            return await ExecuteStoredProcedure("GetStatisticsByPeriod", parms).ConfigureAwait(false);
        }
    }
}