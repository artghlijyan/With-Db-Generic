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

        public void ExecuteInsert(string query, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection != null && connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public int ExecuteUpdate(string query, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection != null && connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                        return (int)cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public bool ExecuteDelete(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
