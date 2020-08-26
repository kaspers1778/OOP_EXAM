using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Ex3
{
    class ConnectionSingleton
    {
        private static ConnectionSingleton instance;
        private readonly SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        private ConnectionSingleton()
        {

        }

        public static ConnectionSingleton getInstance()
        {
            if(instance == null)
            {
                instance = new ConnectionSingleton();
            }
            return instance;
        }

        public SqlConnection GetConnection()
        {

            try
            {
                connection.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Not connected : " + e.ToString());
                Console.ReadLine();
            }
            return connection;
        }
    }
}
