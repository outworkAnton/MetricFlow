using System;
using System.Collections.Generic;
using System.Linq;
using MetricFlow.Models;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{
    public class RevisionBLL
    {
        private const string TABLE_NAME = "Revisions";

        public void SaveLocalRevision(string revisionId, DateTime modifieDateTime, long size)
        {
            var parms = new Dictionary<string, dynamic>
            {
                {"Id", revisionId},
                {"Modified", modifieDateTime},
                {"Size", size}
            };
            InsertRowsIntoTable(TABLE_NAME, parms);
        }

        public DatabaseRevision GetLocalRevision(string id)
        {
            try
            {
                return ConvertFromDAL<DatabaseRevision>(SelectFromByValue(TABLE_NAME, "Id", id))
                        .FirstOrDefault();
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}