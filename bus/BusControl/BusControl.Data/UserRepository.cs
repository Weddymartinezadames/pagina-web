using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace BusControl.Data
{
    public class UserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository()
        {
            _connection = DbConnection.Instance.GetConnection();
        }

        public List<string> GetAll()
        {
            List<string> usuarios = new List<string>();
            string query = "SELECT Nombre FROM Usuarios";

            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(reader["Nombre"].ToString());
            }

            reader.Close();
            _connection.Close();

            return usuarios;
        }
    }
}
