using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace BusControl.Business
{
    public class UsuarioDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BusControlDB"].ConnectionString;

        public List<Usuario> ObtenerUsuarios()
        {
            var lista = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Nombre, Correo, Clave FROM Usuarios";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Correo = reader.GetString(2),
                        Clave = reader.GetString(3)
                    });
                }
            }
            return lista;
        }

        public void InsertarUsuario(string nombre, string correo, string clave)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Usuarios (Nombre, Correo, Clave) VALUES (@Nombre, @Correo, @Clave)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarUsuario(int id, string nombre, string correo, string clave)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Usuarios SET Nombre=@Nombre, Correo=@Correo, Clave=@Clave WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Usuarios WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
