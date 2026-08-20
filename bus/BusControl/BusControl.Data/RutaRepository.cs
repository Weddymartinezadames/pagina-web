using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BusControl.Data
{
    public class RutaRepository
    {
        private readonly SqlConnection _connection;

        public RutaRepository()
        {
            _connection = DbConnection.Instance.GetConnection();
        }

        public List<string> GetAll()
        {
            List<string> rutas = new List<string>();
            string query = "SELECT Nombre FROM Rutas";

            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rutas.Add(reader["Nombre"].ToString());
            }

            reader.Close();
            _connection.Close();

            return rutas;
        }
    }
}
