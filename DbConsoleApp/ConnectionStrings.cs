namespace DbConsoleApp.GlobalObjects
{
    public static class ConnectionStrings
    {
        public static readonly string HomeSqlConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = LocalDB; Integrated Security = True;";

        public static readonly string MicSqlConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MicSchool;Integrated Security=True;";
    }
}