using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbFramework.DbHelper
{
    class DbContext
    {
        private readonly string connectionString;

        public DbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<IDataReader> ExecuteSelect(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
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

        public void ExecuteInsert(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //TODO need to be added with parameter
                }
                
            }
        }
    }
}
