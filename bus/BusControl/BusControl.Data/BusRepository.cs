using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BusControl.Data
{
    public class BusRepository
    {
        private readonly SqlConnection _connection;

        public BusRepository()
        {
            _connection = DbConnection.Instance.GetConnection();
        }

        public List<string> GetAll()
        {
            List<string> buses = new List<string>();
            string query = "SELECT Nombre FROM Buses";

            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                buses.Add(reader["Nombre"].ToString());
            }

            reader.Close();
            _connection.Close();

            return buses;
        }
    }
}
