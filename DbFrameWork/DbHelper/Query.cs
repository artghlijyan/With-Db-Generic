using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DbFramework.DbHelper
{
    static class Query
    {
        private static readonly string selectQuery = "SELECT * FROM {0}";
        private static readonly string insertQuery = "INSERT into {0} ({1}) VALUES ({2}); SELECT CAST(scope_identity() AS int)";
        private static readonly string updateQuery = "UPDATE {0} Set {1} = {2} WHERE Id = @{4}";
        private static readonly string deleteQuery = "Delete FROM {0} WHERE {1}";

        public static string SelectBuilder(string tableName)
        {
            return string.Format(selectQuery, tableName);
        }

        public static string InsertBuilder(string tableName, Dictionary<string, object> param)
        {
            Dictionary<string, string> columValue = new Dictionary<string, string>();
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var parameter in param)
            {
                columns.Append(parameter.Key).Append(",");
                values.Append("@").Append(parameter.Value).Append(",");
            }

            string colum = columns.ToString().TrimEnd(',');
            string value = values.ToString().TrimEnd(',');

            return string.Format(insertQuery, tableName, colum, value);
        }
    }
}
