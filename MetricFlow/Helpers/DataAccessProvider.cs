using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace MetricFlow.Helpers
{
    public static class DataAccessProvider
    {
        #region Internal logic methods

        static DataTable ExecuteSelectQuery(string query)
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

        static void ExecuteUpsertQuery(string query)
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

        private static string ConvertDateTimeToDBFormat(DateTime datetime)
        {
            var dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour,
                datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        #endregion

        public static IEnumerable<T> ConvertFromDAL<T>(DataTable dataFromDAL) where T : new()
        {
            var result = new List<T>();

            foreach (DataRow dataRow in dataFromDAL.Rows)
            {
                var parms = new List<dynamic>(dataRow.ItemArray).ToArray();
                result.Add((T) Activator.CreateInstance(typeof(T), parms));
            }

            return result;
        }

        #region Stored procedures

        public static DataTable ExecuteStoredProcedure(string name, StoredProcedureParameters paramValues)
        {
            var spBody = SelectFromByValue("StoredProcedures", "Name", name).Rows[0].ItemArray[1].ToString();
            var query = paramValues.InjectValues(spBody);
            return ExecuteSelectQuery(query);
        }

        public class StoredProcedureParameters
        {
            private readonly Dictionary<string, dynamic> _innerCollection = new Dictionary<string, dynamic>();

            public void AddParameter<T>(string name, T value)
            {
                _innerCollection.Add(name, value);
            }

            public void DeleteParameter(string name)
            {
                _innerCollection.Remove(name);
            }

            public string InjectValues(string bodyOfStoredProcedure)
            {
                foreach (var parameter in _innerCollection)
                {
                    var parmName = "@" + parameter.Key;
                    var parmValue = parameter.Value;
                    var newParmValue = string.Empty;
                    var dlm = string.Empty;
                    var oper = " = ";

                    switch (parmValue)
                    {
                        case string s:
                            dlm = "'";
                            newParmValue = parmValue as string;
                            break;
                        case IEnumerable en:
                            oper = " IN ";
                            newParmValue += "(";
                            if ((parmValue[0] as string) != null)
                            {
                                dlm = "'";
                            }

                            newParmValue += string.Join($"{dlm}, {dlm}", parmValue);
                            newParmValue += ")";
                            break;
                    }

                    var injectParam = parameter.Key + oper + dlm + newParmValue + dlm;

                    bodyOfStoredProcedure = bodyOfStoredProcedure.Replace(parmName, injectParam);
                }

                return bodyOfStoredProcedure;
            }
        }

        #endregion

        #region Select Statements

        public static DataTable SelectAllFrom(string tableName)
        {
            var query = $"SELECT * FROM {tableName};";
            return ExecuteSelectQuery(query);
        }

        public static DataTable SelectFromByValue<T>(string tableName, string paramName, T paramValue)
        {
            var dlm = (typeof(T) == Type.GetType("System.String") || (typeof(T) == Type.GetType("System.DateTime")))
                ? "'"
                : string.Empty;
            var query = $"SELECT * FROM {tableName} WHERE {paramName} = {dlm}{paramValue}{dlm};";
            return ExecuteSelectQuery(query);
        }

        #endregion

        #region Insert Statements

        public static void InsertRowsIntoTable(string tableName, Dictionary<string, dynamic> rows)
        {
            if (!rows.Any()) throw new ArgumentNullException();
            var columns = rows.Keys.ToList();
            var query = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES(";
            foreach (var value in rows.Values.ToList())
            {
                var dlm = string.Empty;
                string dtValue = null;
                switch (value)
                {
                    case string str:
                        dlm = "'";
                        break;
                    case DateTime dt:
                        dlm = "'";
                        dtValue = ConvertDateTimeToDBFormat(value);
                        break;
                }

                query += dlm + (dtValue ?? value) + dlm + ", ";
            }

            query = query.Remove(query.Length - 2, 2);
            query += $");";
            ExecuteUpsertQuery(query);
        }

        #endregion
    }
}