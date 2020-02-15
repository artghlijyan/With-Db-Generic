using System;

namespace DbFramework.DbHelper
{
    static class Query
    {
        public enum QueryType
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4,
        }

        //private static readonly string SelectQuery = "SELECT * FROM {0}; CAST(scope_identity() AS int)";
        private static readonly string selectQuery = "SELECT * FROM {0}";
        private static readonly string insertQuery = "INSERT into {0} ({1}) VALUES ({2})";
        private static readonly string updateQuery = "UPDATE {0} Set {1} = {2} WHERE Id = @{4}";
        private static readonly string deleteQuery = "Delete FROM {0} WHERE {1}";

        public static string SelectByQuery(QueryType type, string tableName)
        {
            return QueryBuilder(type, tableName);
        }

        static string QueryBuilder(QueryType type, string tableName)
        {
            switch (type)
            {
                case QueryType.Select: return string.Format(selectQuery, tableName);
                case QueryType.Insert: return string.Empty;
                case QueryType.Update: return string.Empty;
                case QueryType.Delete: return string.Empty;
                default: throw new NotImplementedException("Unable to execute query");
            }
        }
    }
}
