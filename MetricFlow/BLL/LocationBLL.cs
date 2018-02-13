using System.Collections.Generic;
using System.Data;
using System.Linq;
using MetricFlow.Helpers;
using MetricFlow.Models;

namespace MetricFlow.BLL
{
    public class LocationBLL
    {
        private const string TABLE_NAME = "Locations";

        public DataTable GetAllLocations()
        {
            return DataAccessProvider.SelectAllFrom(TABLE_NAME);
        }

        public DataTable GetLocationById(int locationId)
        {
            return DataAccessProvider.SelectFromByValue(TABLE_NAME, "Id", locationId);
        }

        public Location CreateLocation(string locationName)
        {
            var location = new List<string> { locationName };
            DataAccessProvider.InsertValuesIntoColumn(TABLE_NAME, "Name", location);
            var data = DataAccessProvider.SelectFromByValue(TABLE_NAME, "Name", locationName);
            return data == null ? null : DataAccessProvider.ConvertFromDAL<Location>(data).FirstOrDefault();
        }
    }
}