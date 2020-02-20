using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbFramework.DbHelper
{
    static class QueryBuilder
    {
        private static readonly string selectQuery = "SELECT * FROM [{0}]";
        private static readonly string insertQuery = "INSERT into {0} ({1}) VALUES ({2}); SELECT CAST(scope_identity() AS int)";
        private static readonly string updateQuery = "UPDATE {0} Set {1} WHERE Id = @{2}";
        private static readonly string deleteQuery = "Delete FROM {0} WHERE Id = {1}";

        public static string Select(string tableName)
        {
            return string.Format(selectQuery, tableName);
        }

        public static string Insert(string tableName, IEnumerable<string> parameterNames)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var parameterName in parameterNames)
            {
                columns.Append('[').Append(parameterName).Append(']').Append(",");
                values.Append('@').Append(parameterName).Append(",");
            }

            return string.Format(
                insertQuery, tableName, columns.ToString().TrimEnd(','), values.ToString().TrimEnd(','));
        }

        public static string Update(string tableName, IEnumerable<string> parameterNames)
        {
            StringBuilder columnValue = new StringBuilder();

            foreach (var parameterName in parameterNames)
            {
                columnValue.Append('[').Append(parameterName).Append(']')
                    .Append('=').Append('@').Append(parameterName).Append(',');
            }

            return string.Format(
                updateQuery, tableName, columnValue.ToString().TrimEnd(','), parameterNames.Single(n => n == "Id"));
        }

        public static string Delete(string modelName, int modelId)
        {
            return string.Format(deleteQuery, modelName, modelId);
        }
    }
}
