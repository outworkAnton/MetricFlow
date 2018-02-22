using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;
using static MetricFlow.Helpers.DataAccessProvider;

namespace MetricFlow.BLL
{

    public class RevisionBLL
    {
        private const string TABLE_NAME = "Revisions";

        public void InsertRevision(Revision revision)
        {
            InsertRowsIntoTable(TABLE_NAME, );
        }
    }
}