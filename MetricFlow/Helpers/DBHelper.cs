using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace MetricFlow.Helpers
{
    public static class DBHelper
    {
        public static DataTable ExecuteSelectQuery(string query)
        {
            var tmpTable = new DataTable();
            using (var connection = new SQLiteConnection(App.DbConnectionString).OpenAndReturn())
            {
                try
                {
                    var command = connection.CreateCommand().CommandText = query;
                    var adapter = new SQLiteDataAdapter(command, connection);
                    adapter.Fill(tmpTable);
                }
                catch (SQLiteException exception)
                {
                    Debug.WriteLine(exception);
                }
            }

            return tmpTable;
        }

        public static void ExecuteUpsertQuery(string query)
        {
            using (var connection = new SQLiteConnection(App.DbConnectionString).OpenAndReturn())
            {
                var transaction = connection.BeginTransaction();
                try
                {
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException exception)
                {
                    transaction.Rollback();
                    Debug.WriteLine(exception);
                }
                transaction.Commit();
            }
        }

        public static IEnumerable<T> ConvertFromDAL<T>(DataTable dataFromDAL) where T : new()
        {
            var result = new List<T>();

            foreach (DataRow dataRow in dataFromDAL.Rows)
            {
                var parms = new List<object>(dataRow.ItemArray);
                result.Add((T)Activator.CreateInstance(typeof(T), parms));
            }

            return result;
        }
    }
}