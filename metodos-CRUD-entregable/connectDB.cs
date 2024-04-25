//using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient;
using System;

namespace metodos_CRUD_entregable
{
    internal class ConnectDB
    {

        public static MySqlConnection connectDB()
        {

            string server = "bvu6iefchao8afjfqbg3-mysql.services.clever-cloud.com";
            string db = "bvu6iefchao8afjfqbg3";
            string user = "uo1meqplklb3vt7f";
            string password = "sv6mZgGCLjxcmwYDYC7T";

            string dbConnected = "Database=" + db + "; Data Source=" + server + "; User Id=" + user + "; Password=" + password + "";

            try
            {
                MySqlConnection connection = new MySqlConnection(dbConnected);


                Console.WriteLine("connected to database");
                return connection;

            }
            catch (MySqlException ex)
            {

                Console.WriteLine("Error:  " + ex.Message);
                return null;
            }
        }

    }
}
