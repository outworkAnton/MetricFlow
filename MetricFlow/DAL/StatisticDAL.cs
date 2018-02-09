using System.Data;
using MetricFlow.Helpers;

namespace MetricFlow.DAL
{
    public class StatisticDAL
    {
        public DataTable GetAllStatistics()
        {
            var query = "SELECT " +
                        "Id, " +
                        "Year, " +
                        "Month, " +
                        "LocationId, " +
                        "ServiceId, " +
                        "MetricId, " +
                        "FormulaId, " +
                        "Value " +
                        "FROM Statistics";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public DataTable GetStatisticsByLocation(int locationId)
        {
            var query = "SELECT " +
                        "Id, " +
                        "Year, " +
                        "Month, " +
                        "LocationId, " +
                        "ServiceId, " +
                        "MetricId, " +
                        "FormulaId, " +
                        "Value " +
                        "FROM Statistics" +
                        $"WHERE LocationId={locationId}";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public DataTable GetStatisticsByMonth(int year, int month)
        {
            var query = "SELECT " +
                        "Id, " +
                        "Year, " +
                        "Month, " +
                        "LocationId, " +
                        "ServiceId, " +
                        "MetricId, " +
                        "FormulaId, " +
                        "Value " +
                        "FROM Statistics" +
                        $"WHERE Year = {year}" +
                        $"AND Month = {month}";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public DataTable GetStatisticsByFewMonths(int year, int[] months)
        {
            var query = "SELECT " +
                        "Id, " +
                        "Year, " +
                        "Month, " +
                        "LocationId, " +
                        "ServiceId, " +
                        "MetricId, " +
                        "FormulaId, " +
                        "Value " +
                        "FROM Statistics" +
                        $"WHERE Year = {year}" +
                        $"AND Month IN ({months})";
            return DBHelper.ExecuteSelectQuery(query);
        }
    }
}