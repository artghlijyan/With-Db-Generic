using System.Collections.Generic;
using System.Text;

namespace DbFramework.DbHelper
{
    static class Query
    {
        private static readonly string selectQuery = "SELECT * FROM [{0}]";
        private static readonly string insertQuery = "INSERT into {0} ({1}) VALUES ({2}); SELECT CAST(scope_identity() AS int)";
        private static readonly string updateQuery = "UPDATE {0} Set {1} = {2} WHERE Id = @{4}";
        private static readonly string deleteQuery = "Delete FROM {0} WHERE {1}";

        public static string SelectBuilder(string tableName)
        {
            return string.Format(selectQuery, tableName);
        }

        public static string InsertBuilder(string tableName, IEnumerable<string> parameterNames)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var parameter in parameterNames)
            {
                columns.Append('[').Append(parameter).Append(']').Append(",");
                values.Append('@').Append(parameter).Append(",");
            }

            string colum = columns.ToString().TrimEnd(',');
            string value = values.ToString().TrimEnd(',');

            string query = string.Format(insertQuery, tableName, colum, value);
            return query;
        }
    }
}
