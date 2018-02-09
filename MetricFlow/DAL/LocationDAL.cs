using System.Data;
using System.Linq;
using MetricFlow.Helpers;
using MetricFlow.Models;

namespace MetricFlow.DAL
{
    public class LocationDAL
    {
        public DataTable GetAllLocations()
        {
            var query = "SELECT * " +
                        "FROM Locations;";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public DataTable GetLocationById(int locationId)
        {
            var query = "SELECT * " +
                        "FROM Locations " +
                        $"WHERE Id={locationId};";
            return DBHelper.ExecuteSelectQuery(query);
        }

        public Location CreateLocation(string locationName)
        {
            var query = "INSERT INTO Locations (Name) " +
                        $"VALUES ( Name: '{locationName}');";
            DBHelper.ExecuteUpsertQuery(query);
            query = "SELECT * " +
                    "FROM Locations " +
                    $"WHERE Name='{locationName}';";
            var data = DBHelper.ExecuteSelectQuery(query);
            return data == null ? null : DBHelper.ConvertFromDAL<Location>(data).FirstOrDefault();
        }
    }
}