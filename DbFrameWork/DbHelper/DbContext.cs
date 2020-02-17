using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        public void ExecuteInsert(string query, Dictionary<string, object> parameters)
        {
            SqlParameter sqlParameter;
            SqlParameter[] sqlParameters = new SqlParameter[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter = new SqlParameter(parameters.Keys.ElementAt(i), parameters.Values.ElementAt(i));
                sqlParameters[i] = sqlParameter;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddRange(sqlParameters);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}
