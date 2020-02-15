using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbFramework.DbConnector
{
    public class DbContext
    {
        private readonly string connectionString;

        public DbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<IDataReader> ExecuteSelect(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader;
                        }
                    }
                }
            }
        }
    }
}
