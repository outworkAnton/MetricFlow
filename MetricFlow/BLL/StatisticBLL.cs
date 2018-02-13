using System.Data;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class StatisticBLL
    {
        private const string TABLE_NAME = "Statistics";

        public DataTable GetAllStatistics()
        {
            return SelectAllFrom(TABLE_NAME);
        }

        public DataTable GetStatisticsByLocation(int locationId)
        {
            return SelectFromByValue(TABLE_NAME, "LocationId", locationId);
        }

        public DataTable GetStatisticsByPeriod(int locationId, int year, int[] months)
        {
            var parms = new StoredProcedureParameters();
            parms.AddParameter("LocationId", locationId);
            parms.AddParameter("Year", year);
            parms.AddParameter("Month", months);
            return ExecuteStoredProcedure("GetStatisticsByPeriod", parms);
        }
    }
}