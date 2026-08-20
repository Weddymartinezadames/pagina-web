using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusControl.Business
{
    public class RutaDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BusControlDB"].ConnectionString;

        public List<Ruta> ObtenerRutas()
        {
            var lista = new List<Ruta>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Origen, Destino, Duracion FROM Rutas";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Ruta
                    {
                        Id = reader.GetInt32(0),
                        Origen = reader.GetString(1),
                        Destino = reader.GetString(2),
                        Duracion = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
            }
            return lista;
        }

        public void InsertarRuta(string origen, string destino, string duracion = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Rutas (Origen, Destino, Duracion) VALUES (@Origen, @Destino, @Duracion)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Origen", origen);
                cmd.Parameters.AddWithValue("@Destino", destino);
                cmd.Parameters.AddWithValue("@Duracion", string.IsNullOrEmpty(duracion) ? (object)DBNull.Value : duracion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarRuta(int id, string origen, string destino, string duracion = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Rutas SET Origen = @Origen, Destino = @Destino, Duracion = @Duracion WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Origen", origen);
                cmd.Parameters.AddWithValue("@Destino", destino);
                cmd.Parameters.AddWithValue("@Duracion", string.IsNullOrEmpty(duracion) ? (object)DBNull.Value : duracion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarRuta(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Rutas WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}