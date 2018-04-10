using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricFlow.Models;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class RevisionBLL
    {
        private const string TABLE_NAME = "Revisions";

        public async Task SaveLocalRevision(string revisionId, DateTime modifieDateTime, long size)
        {
            var parms = new Dictionary<string, dynamic>
            {
                {"Id", revisionId},
                {"Modified", modifieDateTime},
                {"Size", size}
            };
            await InsertRowsIntoTable(TABLE_NAME, parms).ConfigureAwait(false);
        }

        public async Task<DatabaseRevision> GetLocalRevision(string id)
        {
            try
            {
                return ConvertFromDAL<DatabaseRevision>(await SelectFromByValue(TABLE_NAME, "Id", id).ConfigureAwait(false))
                        .FirstOrDefault();
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}