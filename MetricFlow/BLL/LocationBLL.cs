using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MetricFlow.Helpers;
using MetricFlow.Models;

namespace MetricFlow.BLL
{
    public class LocationBLL
    {
        private const string TABLE_NAME = "Locations";

        public async Task<DataTable> GetAllLocations()
        {
            return await DataAccessProvider.SelectAllFrom(TABLE_NAME).ConfigureAwait(false);
        }

        public async Task<DataTable> GetLocationById(int locationId)
        {
            return await DataAccessProvider.SelectFromByValue(TABLE_NAME, "Id", locationId).ConfigureAwait(false);
        }

        public async Task<Location> CreateLocation(string locationName)
        {
            var parms = new Dictionary<string, dynamic>
            {
                {"Name", locationName}
            };
            await DataAccessProvider.InsertRowsIntoTable(TABLE_NAME, parms).ConfigureAwait(false);
            var data = await DataAccessProvider.SelectFromByValue(TABLE_NAME, "Name", locationName).ConfigureAwait(false);
            return data == null ? null : DataAccessProvider.ConvertFromDAL<Location>(data).FirstOrDefault();
        }
    }
}