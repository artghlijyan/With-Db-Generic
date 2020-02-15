namespace DbFramework.DbConnector
{
    public static class Query
    {
        private static readonly string SelectQuery = "SELECT * FROM {0}";
        private static readonly string InsertQuery = "INSERT into {0} ({1}) VALUES ({2})";
        private static readonly string UpdateQuery = "UPDATE {0} Set {1} = {2} WHERE Id = @{4}";
        private static readonly string DeleteQuery = "Delete FROM {0} WHERE {1}";
        
        public static string Select()
        {
            return string.Empty;
        }

        static void QueryBuilder()
        {

        }
    }
}
