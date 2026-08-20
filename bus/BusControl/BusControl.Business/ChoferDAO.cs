using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace BusControl.Business
{
    public class ChoferDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BusControlDB"].ConnectionString;

        public List<Chofer> ObtenerChoferes()
        {
            var lista = new List<Chofer>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Licencia, Telefono FROM Choferes";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Chofer
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Licencia = reader.GetString(2),
                        Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
            }
            return lista;
        }

        public void InsertarChofer(string nombre, string licencia, string telefono)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Choferes (Nombre, Licencia, Telefono) VALUES (@Nombre, @Licencia, @Telefono)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Licencia", licencia);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarChofer(int id, string nombre, string licencia, string telefono)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Choferes SET Nombre=@Nombre, Licencia=@Licencia, Telefono=@Telefono WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Licencia", licencia);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarChofer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Choferes WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
